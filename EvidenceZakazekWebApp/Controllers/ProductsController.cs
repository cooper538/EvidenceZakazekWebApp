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

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Suppliers = _context.Suppliers.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            var product = new Product()
            {
                Name = viewModel.Name,
                OrderNumber = viewModel.OrderNumber,
                TypeName = viewModel.TypeName,
                Price = viewModel.Price,
                SupplierId = viewModel.Supplier
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}