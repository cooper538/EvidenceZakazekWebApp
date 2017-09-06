using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Dtos.Interfaces;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductCategoriesController : Controller
    {
        ApplicationDbContext _context;
        IMapper _mapper;

        public ProductCategoriesController()
        {
            _context = new ApplicationDbContext();
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        public ActionResult Index()
        {
            var productCategories = _context.ProductCategories.ToList();

            var productCategoryDtos = _mapper.Map<List<ProductCategory>, List<ProductCategoryDto>>(productCategories);
            var productCategoryTableDtos = _mapper.Map<List<ProductCategoryDto>, List<ProductCategoryTableDto>>(productCategoryDtos);

            var CrudtableDtos = new Collection<ICrudTableDto>();
            productCategoryTableDtos.ForEach(pctd => CrudtableDtos.Add(pctd));

            var viewModel = new CrudTableViewModel()
            {
                Heading = "Kategorie",
                ControllerName = "productCategories",
                CrudTableDtos = CrudtableDtos
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductCategoryFormViewModel()
            {
                Heading = "Přidej kategorii"
            };

            return View("ProductCategoryForm", viewModel);
        }

        // Solution by https://blog.rsuter.com/asp-net-mvc-how-to-implement-an-edit-form-for-an-entity-with-a-sortable-child-collection/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductCategoryForm", viewModel);
            }

            var productCategory = _mapper
                .Map<ProductCategoryFormViewModel, ProductCategory>(viewModel);

            _context.ProductCategories.Add(productCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var productCategory = _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .Single(pc => pc.Id == id);

            var viewModel = new ProductCategoryFormViewModel
            {
                Heading = $"Editace kategorie s id:{productCategory.Id}"
            };

            _mapper.Map(productCategory, viewModel);

            return View("ProductCategoryForm", viewModel);
        }

        public ActionResult Update(ProductCategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductCategoryForm", viewModel);
            }

            var productCategory = _context.ProductCategories
                .Include(pc => pc.Products)
                .Include(pc => pc.PropertyDefinitions)
                .Single(pc => pc.Id == viewModel.Id);

            productCategory.Name = viewModel.Name;

            var oldPropetyDefinitions = productCategory.PropertyDefinitions;
            var newPropertyDefinitions = viewModel.PropertyDefinitions.Select(pd =>
                new PropertyDefinition {
                    Id = pd.Id,
                    Name = pd.Name,
                    MeasureUnit = pd.MeasureUnit
                }).ToList();

            // Check of deleted properties
            foreach (var oldPropertyDefinition in oldPropetyDefinitions.ToList())
            {
                if (!newPropertyDefinitions.Any(npd => npd.Id == oldPropertyDefinition.Id))
                {
                    var propertyDefinitionForDelete = _context.PropertyDefinitions
                        .Include(pdfd => pdfd.PropertyValues)
                        .Single(pdfd => pdfd.Id == oldPropertyDefinition.Id);

                    _context.PropertyValues.RemoveRange(propertyDefinitionForDelete.PropertyValues);
                    _context.PropertyDefinitions.Remove(propertyDefinitionForDelete);
                }
            }

            // Check of edited or added properties
            foreach (var newPropertyDefinition in newPropertyDefinitions)
            {
                if (oldPropetyDefinitions.Any(opd => opd.Id == newPropertyDefinition.Id))
                {
                    var oldPropertyDefinition = oldPropetyDefinitions.Single(opd => opd.Id == newPropertyDefinition.Id);
                    oldPropertyDefinition.Name = newPropertyDefinition.Name;
                    oldPropertyDefinition.MeasureUnit = newPropertyDefinition.MeasureUnit;
                }
                else
                {
                    productCategory.PropertyDefinitions.Add(newPropertyDefinition);

                    // With new propertyDefinition, have to be add specific productValue to every item in category                       
                    foreach (var product in productCategory.Products)
                    {
                        newPropertyDefinition.PropertyValues.Add(
                            new PropertyValue {
                                ProductId = product.Id,
                                Value = "(Nezadáno)"
                            });
                    }
                    
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var viewModel = new DetailViewModel()
            {
                Heading = "Detail kategorie s id:" + id,
                ControllerName = "ProductCategories"
            };

            var productCategory = _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .SingleOrDefault(pc => pc.Id == id);

            var staticProperties = new Dictionary<string, string>()
            {
                //{ "Id", productCategory.Id.ToString() },
                { "Jméno", productCategory.Name },
            };

            var dynamicProperties = productCategory.PropertyDefinitions
                .ToDictionary(dp => dp.Name, dp => dp.MeasureUnit);

            var properties = staticProperties.Union(dynamicProperties)
                .ToDictionary(p => p.Key, p => p.Value);

            viewModel.Properties = properties;

            return View("Detail", viewModel);
        }

        public ActionResult GetPropertyDefinitionForm()
        {
            return PartialView("Partial/PropertyDefinitionForm", new PropertyDefinitionFormViewModel());
        }

        [HttpGet]
        public ActionResult GetPropertyValuesForm(int categoryId)
        {
            var propertyDefinitions = _context.PropertyDefinitions
                .Where(pd => pd.ProductCategoryId == categoryId)
                .ToList();

            List<PropertyValueFormViewModel> propertyValues = propertyDefinitions.Select(
                pd => new PropertyValueFormViewModel()
                {
                    PropertyDefinitionId = pd.Id,
                    PropertyDefinitionName = pd.Name,
                    MeasureUnit = pd.MeasureUnit,
                    Value = "",
                }).ToList();

            return PartialView("Partial/PropertyValuesForm", propertyValues);
        }
    }
}