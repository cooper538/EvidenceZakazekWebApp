using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class PropertyDefinitionsController : Controller
    {
        [HttpGet]
        public ActionResult GetNewPropertyDefinitionForm()
        {
            return PartialView("/Views/PropertyDefinition/PropertyDefinitionForm.cshtml", new PropertyDefinitionFormViewModel());
        }
    }
}