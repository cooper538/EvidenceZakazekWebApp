using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Persistence;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.Products.GetProducts());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}