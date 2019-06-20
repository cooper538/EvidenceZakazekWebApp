using AutoMapper;
using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Helpers.FlashMessagesHelper;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public ActionResult Index(int id = 1)
        {
            if (_unitOfWork.ProductCategories.GetCategory(id) == null)
            {
                this.AddFlashMessage(FlashMessageType.Warning, $"Kategorie nebyla nalezena. (CategoryId: {id})");
                RouteData.Values.Remove(nameof(id));
                return RedirectToAction(nameof(Index));
            }

            var products = _unitOfWork.Products.GetProductsWithPropertiesByCategory(id);

            var viewModel = new CrudTableViewModel
            {
                Heading = "Produkty",
                CrudRowViewModels = _mapper.Map<IEnumerable<CrudRowViewModel>>(products)
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Heading = "Přidej produkt"
            };
            LoadDataForSelects(viewModel);

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!HasValidProperties(viewModel))
                    {
                        this.AddFlashMessage(
                            FlashMessageType.Warning,
                            "Vlastnosti produktu nebyly správně zadány. " +
                            "Nastala chyba při načítání vlastností podle kategorie");

                        LoadDataForSelects(viewModel);
                        return View("ProductForm", viewModel);
                    }

                    var product = _mapper.Map<ProductFormViewModel, Product>(viewModel);
                    _unitOfWork.Products.Add(product);
                    _unitOfWork.Complete();
                    this.AddFlashMessage(FlashMessageType.Success, "Produkt byl uložen.");
                }
                else
                {
                    LoadDataForSelects(viewModel);
                    return View("ProductForm", viewModel);
                }

            }
            catch (DbEntityValidationException ex)
            {
                this.AddFlashMessage(FlashMessageType.Danger, "Produkt nebyl uložen. Kontaktujte podporu.");
                // Todo: přidat logování
            }

            return RedirectToAction(nameof(Index), "Products");
        }

        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            if (product == null)
            {
                this.AddFlashMessage(FlashMessageType.Warning, $"Produkt nebyl nalezen. (ProductId: {id})");
                RouteData.Values.Remove(nameof(id));
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductFormViewModel
            {
                Heading = $"Editace produktu s id:{product.Id}",
            };
            LoadDataForSelects(viewModel);

            _mapper.Map(product, viewModel);

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(ProductFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _unitOfWork.Products.GetProductWithProperties(viewModel.Id);

                    // Remove old PropertyValues
                    _unitOfWork.PropertyValues.RemoveValuesByProduct(product.Id);

                    // Update Product and add new PropertyValues
                    product.Modify(_mapper.Map<Product>(viewModel));

                    _unitOfWork.Complete();
                    this.AddFlashMessage(FlashMessageType.Success, "Produkt byl uložen.");
                }
                else
                {
                    LoadDataForSelects(viewModel);
                    return View("ProductForm", viewModel);
                }
            }
            catch (DbEntityValidationException ex)
            {
                this.AddFlashMessage(FlashMessageType.Danger, "Produkt nebyl uložen. Kontaktujte podporu.");
                // Todo: přidat logování
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            if (product == null)
            {
                this.AddFlashMessage(FlashMessageType.Warning, $"Produkt nebyl nalezen. (ProductId: {id})");
                RouteData.Values.Remove(nameof(id));
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new DetailViewModel
            {
                Heading = "Detail produktu s id:" + id
            };

            _mapper.Map(product, viewModel);

            return View(viewModel);
        }

        private void LoadDataForSelects(ProductFormViewModel viewModel)
        {
            viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
            viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
        }

        private bool HasValidProperties(ProductFormViewModel viewModel)
        {
            var propertyDefinitionsByCategory = _unitOfWork.PropertyDefinitions
                .GetDefinitionsByCategory(viewModel.ProductCategoryId);

            return propertyDefinitionsByCategory.All(pd =>
                viewModel.PropertyValues.Any(pv => pv.PropertyDefinitionId == pd.Id));
        }
    }
}