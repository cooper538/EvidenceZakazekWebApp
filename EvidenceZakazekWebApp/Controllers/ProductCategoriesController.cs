using AutoMapper;
using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var productCategories = _unitOfWork.ProductCategories.GetCategories();

            var crudRowViewModels = _mapper.Map<IEnumerable<CrudRowViewModel>>(productCategories);

            var viewModel = new CrudTableViewModel
            {
                Heading = "Kategorie",
                CrudRowViewModels = crudRowViewModels
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductCategoryFormViewModel
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

            return RedirectToAction(nameof(Index));
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
            var productCategory = _unitOfWork.ProductCategories
                .GetCategoryWithProductsAndProperties(viewModel.Id);

            // Update PropertyDefinitions for Category
            _unitOfWork.PropertyDefinitions.UpdateDefinitionsForProductCategory(
                productCategory.Id,
                _mapper.Map<ICollection<PropertyDefinition>>(viewModel.PropertyDefinitions));

            // Update
            productCategory.Modify(_mapper.Map<ProductCategory>(viewModel));

            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(int id)
        {
            var productCategory = _unitOfWork.ProductCategories.GetCategoryWithDefinitions(id);

            var viewModel = new DetailViewModel
            {
                Heading = "Detail kategorie s id:" + id
            };

            _mapper.Map(productCategory, viewModel);

            return View(viewModel);
        }
    }
}