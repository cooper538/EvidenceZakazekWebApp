using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductCategoriesController : Controller
    {
        ApplicationDbContext _context;

        public ProductCategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new ProductCategoryFormViewModel();

            return View(viewModel);
        }

        // Solution by https://blog.rsuter.com/asp-net-mvc-how-to-implement-an-edit-form-for-an-entity-with-a-sortable-child-collection/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var productCategory = new ProductCategory
            {
                Name = viewModel.Name,
                PropertyDefinitions = viewModel.PropertyDefinitions.Select(
                        pdvm => new PropertyDefinition()
                        {
                            Id = pdvm.Id,
                            Name = pdvm.Name,
                            MeasureUnit = pdvm.MeasureUnit
                        }
                    ).ToList()
            };

            _context.ProductCategories.Add(productCategory);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit()
        {
            //var productCategory = _context.ProductCategories
            //    .Include(pc => pc.PropertyDefinitions)
            //    .First();

            //var viewModel = new ProductCategoryFormViewModel
            //{
            //    Name = productCategory.Name,
            //    ProductDefinitions = productCategory.PropertyDefinitions.Select(
            //            pd => new PropertyDefinitionFormViewModel()
            //            {
            //                Id = pd.Id,
            //                Name = pd.Name,
            //                MeasureUnit = pd.MeasureUnit
            //            }
            //        )
            //};

            return View();
        }

        public ActionResult GetPropertyDefinitionForm()
        {
            return PartialView("Partial/PropertyDefinitionForm", new PropertyDefinitionFormViewModel());
        }
    }
}