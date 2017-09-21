using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Persistence;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductsController : Controller
    {
        UnitOfWork _unitOfWork;
        IMapper _mapper;

        public ProductsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); // Todo: Inject dependency

        }

        public ActionResult Index(int id = 1)
        {
            var products = _unitOfWork.Products.GetProductsWithPropertiesByCategory(id);

            var viewModel = new CrudTableViewModel()
            {
                Heading = "Produkty",
                ControllerName = "products",
                CrudRowViewModels = _mapper.Map<IEnumerable<CrudRowViewModel>>(products)
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Heading = "Přidej produkt",
                Suppliers = _unitOfWork.Suppliers.GetSuppliers(), //ToDo: Změnit typ kolekce ve ViewModelu na IEnumerble? Rozumně, podle smyslu
                ProductCategories = _unitOfWork.ProductCategories.GetCategories()
            };

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

            return RedirectToAction("Index", "Products");
        }

        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetExtendedProduct(id);

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
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _unitOfWork.Suppliers.GetSuppliers();
                viewModel.ProductCategories = _unitOfWork.ProductCategories.GetCategories();
                return View("ProductForm", viewModel);
            }

            var product = _unitOfWork.Products.GetExtendedProduct(viewModel.Id);

            // Delete old PropertyValues
            _unitOfWork.PropertyValues.RemoveValuesByProduct(product.Id);

            // Update Product and add new PropertyValues 
            product.Modify(_mapper.Map<Product>(viewModel));

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var product = _unitOfWork.Products.GetExtendedProduct(id);

            var viewModel = new DetailViewModel()
            {
                Heading = "Detail produktu s id:" + id,
                ControllerName = "Products" // TODO: refaktoring to dynamic name, no magic strings
            };

            _mapper.Map(product, viewModel);

            return View("Detail", viewModel);
        }
    }
}