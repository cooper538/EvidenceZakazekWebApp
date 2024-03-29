﻿using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    [Authorize]
    public class PropertyValuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult GetPropertyValuesFormByCategory(int categoryId)
        {
            var propertyDefinitions = _unitOfWork.PropertyDefinitions
                .GetDefinitionsByCategory(categoryId);

            var propertyValues = propertyDefinitions.Select(
                pd => new PropertyValueFormViewModel
                {
                    PropertyDefinitionId = pd.Id,
                    PropertyDefinitionName = pd.Name,
                    MeasureUnit = pd.MeasureUnit,
                    Value = "",
                }).ToList();

            return PartialView("/Views/PropertyValues/PropertyValueListForm.cshtml", propertyValues);
        }

    }
}