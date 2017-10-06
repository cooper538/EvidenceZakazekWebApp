using AutoMapper;
using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Helpers.FlashMessagesHelper;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System;
using System.Collections.Generic;
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
                this.AddFlashMessage(FlashMessageType.Warning, $"Kategorie s id: {id} nebyla nalezena.");
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
                Heading = "Přidej produkt",
                Suppliers = _unitOfWork.Suppliers.GetSuppliers(),
                ProductCategories = _unitOfWork.ProductCategories.GetCategories()
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            var isException = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<ProductFormViewModel, Product>(viewModel);
                    _unitOfWork.Products.Add(product);
                    _unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    // Todo: Přidat logování
                    isException = true;
                }
            }
            else
            {
                viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
                viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
                return View("ProductForm", viewModel);
            }

            if (isException || !ModelState.IsValid)
                this.AddFlashMessage(FlashMessageType.Danger, "Produkt nebyl uložen. Kontaktujte podporu.");
            else
                this.AddFlashMessage(FlashMessageType.Success, "Produkt byl uložen.");

            return RedirectToAction(nameof(Index), "Products");
        }

        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            if (product == null)
            {
                this.AddFlashMessage(FlashMessageType.Warning, $"Produkt s id: {id} nebyl nalezen.");
                RouteData.Values.Remove(nameof(id));
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductFormViewModel
            {
                Heading = $"Editace produktu s id:{product.Id}",
                Suppliers = _unitOfWork.Suppliers.GetSuppliers(),
                ProductCategories = _unitOfWork.ProductCategories.GetCategories()
            };

            _mapper.Map(product, viewModel);

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(ProductFormViewModel viewModel)
        {
            var isException = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _unitOfWork.Products.GetProductWithProperties(viewModel.Id);

                    // Remove old PropertyValues
                    _unitOfWork.PropertyValues.RemoveValuesByProduct(product.Id);

                    // Update Product and add new PropertyValues
                    product.Modify(_mapper.Map<Product>(viewModel));

                    _unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    // Todo: Přidat logování
                    isException = true;
                }
            }
            else
            {
                viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
                viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
                return View("ProductForm", viewModel);
            }

            if (isException || !ModelState.IsValid)
                this.AddFlashMessage(FlashMessageType.Danger, "Produkt nebyl uložen. Kontaktujte podporu.");
            else
                this.AddFlashMessage(FlashMessageType.Success, "Produkt byl uložen.");

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            if (product == null)
            {
                this.AddFlashMessage(FlashMessageType.Warning, $"Produkt s id: {id} nebyl nalezen.");
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
    }
}