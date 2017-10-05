using AutoMapper;
using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
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

        public ActionResult Index(int id = 100)
        {
            //TODO: IF kategorie nenalezena
            // TODO: Vrací prázdnou kolekci, vyřešit v crud table
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

            // TODO: Try-catche, pokud selže db, navrat na Crud table s flush

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
                viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
                return View("ProductForm", viewModel);
            }

            var product = _mapper.Map<ProductFormViewModel, Product>(viewModel);

            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();

            // TODO: Try-catche, pokud selže db, navrat na formular s flush message, jinak zobrazit hlasku o uspesnem pridani

            return RedirectToAction(nameof(Index), "Products");
        }

        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            // TODO: IF, pokud vrati null, navrat na Crud table s flush


            var viewModel = new ProductFormViewModel
            {
                Heading = $"Editace produktu s id:{product.Id}",
                Suppliers = _unitOfWork.Suppliers.GetSuppliers(),
                ProductCategories = _unitOfWork.ProductCategories.GetCategories()
            };

            // TODO: Try-catche, pokud selže db, navrat na Crud table

            _mapper.Map(product, viewModel);

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
                viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
                return View("ProductForm", viewModel);
            }

            var product = _unitOfWork.Products.GetProductWithProperties(viewModel.Id);

            // Remove old PropertyValues
            _unitOfWork.PropertyValues.RemoveValuesByProduct(product.Id);

            // Update Product and add new PropertyValues
            product.Modify(_mapper.Map<Product>(viewModel));

            _unitOfWork.Complete();

            // TODO: Try-catche, pokud selže db, navrat na formulář s flush

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            var viewModel = new DetailViewModel
            {
                Heading = "Detail produktu s id:" + id
            };

            // TODO: IF, pokud vrati null, navrat na Crud table s flush Produkt nenalezen

            _mapper.Map(product, viewModel);

            return View(viewModel);
        }
    }
}