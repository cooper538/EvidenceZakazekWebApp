using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
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

            var crudRowViewModels = _mapper.Map<List<ProductCategory>, List<CrudRowViewModel>>(productCategories);

            var viewModel = new CrudTableViewModel()
            {
                Heading = "Kategorie",
                ControllerName = "productCategories",
                CrudRowViewModels = crudRowViewModels
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

            var updatedPropertyDefinitions = _mapper.Map<List<PropertyDefinition>>(viewModel.PropertyDefinitions);

            // Check of deleted properties
            foreach (var oldPropertyDefinition in productCategory.PropertyDefinitions.ToList())
            {
                if (!updatedPropertyDefinitions.Any(npd => npd.Id == oldPropertyDefinition.Id))
                {
                    var propertyDefinitionForDelete = _context.PropertyDefinitions
                        .Include(pdfd => pdfd.PropertyValues)
                        .Single(pdfd => pdfd.Id == oldPropertyDefinition.Id);

                    _context.PropertyValues.RemoveRange(propertyDefinitionForDelete.PropertyValues);
                    _context.PropertyDefinitions.Remove(propertyDefinitionForDelete);
                }
            }

            productCategory.Modify(_mapper.Map<ProductCategory>(viewModel));

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var productCategory = _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .SingleOrDefault(pc => pc.Id == id);

            var viewModel = new DetailViewModel()
            {
                Heading = "Detail kategorie s id:" + id,
                ControllerName = "ProductCategories"
            };

            _mapper.Map(productCategory, viewModel);

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