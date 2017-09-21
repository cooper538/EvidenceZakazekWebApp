using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Persistence;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductCategoriesController : Controller
    {
        UnitOfWork _unitOfWork;
        IMapper _mapper;

        public ProductCategoriesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        public ActionResult Index()
        {
            var productCategories = _unitOfWork.ProductCategories.GetCategories();

            var crudRowViewModels = _mapper.Map<IEnumerable<CrudRowViewModel>>(productCategories);

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

            var productCategory = _mapper.Map<ProductCategory>(viewModel);

            _unitOfWork.ProductCategories.Add(productCategory);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var productCategory = _unitOfWork.ProductCategories.GetCategoryWithDefinitions(id);

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

            var productCategory = _unitOfWork.ProductCategories.GetCategoryWithProductsAndProperties(viewModel.Id);

            //TODO: aaIMPORTANT zjednodušit mazanani - pridat do partial view modelu stav, něco jako new, delste updated apod??
            var updatedPropertyDefinitions = _mapper.Map<List<PropertyDefinition>>(viewModel.PropertyDefinitions);

            // Check of deleted properties
            var oldPropertyDefinitions = productCategory.PropertyDefinitions.ToList();
            foreach (var oldPropertyDefinition in oldPropertyDefinitions)
            {
                if (!updatedPropertyDefinitions.Any(pd => pd.Id == oldPropertyDefinition.Id))
                    _unitOfWork.PropertyDefinitions.RemoveWithValues(oldPropertyDefinition.Id);
            }

            productCategory.Modify(_mapper.Map<ProductCategory>(viewModel));

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var productCategory = _unitOfWork.ProductCategories.GetCategoryWithDefinitions(id);

            var viewModel = new DetailViewModel()
            {
                Heading = "Detail kategorie s id:" + id,
                ControllerName = "ProductCategories"
            };

            _mapper.Map(productCategory, viewModel);

            return View("Detail", viewModel);
        }

        // TODO: aaIMPORTANT Nepatří tyto akce do jiného kontrloleru?? Myslím si že Ano
        public ActionResult GetPropertyDefinitionForm()
        {
            return PartialView("Partial/PropertyDefinitionForm", new PropertyDefinitionFormViewModel());
        }

        [HttpGet]
        public ActionResult GetPropertyValuesForm(int categoryId)
        {
            var propertyDefinitions = _unitOfWork.PropertyDefinitions
                .GetDefinitionsByCategory(categoryId);

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