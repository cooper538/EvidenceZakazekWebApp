using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Suppliers = _context.Suppliers.ToList(),
                Categories = _context.ProductCategories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _context.Suppliers.ToList();
                viewModel.Categories = _context.ProductCategories.ToList();
                return View("Create", viewModel);
            }

            var product = new Product()
            {
                Name = viewModel.Name,
                OrderNumber = viewModel.OrderNumber,
                TypeName = viewModel.TypeName,
                Price = viewModel.Price,
                SupplierId = viewModel.Supplier,
                ProductCategoryId = viewModel.Category,
                PropertyValues = viewModel.PropertyValues.Select(
                    pv => new PropertyValue()
                    {
                        Value = pv.Value,
                        PropertyDefinitionId = pv.PropertyDefinitionId
                    }
                ).ToList()
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}