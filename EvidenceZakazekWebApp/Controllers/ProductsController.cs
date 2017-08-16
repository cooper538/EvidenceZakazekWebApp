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
    }
}