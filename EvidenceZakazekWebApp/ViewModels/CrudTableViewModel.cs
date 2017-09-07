using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class CrudTableViewModel
    {
        public string Heading { get; set; }
        public string ControllerName { get; set; }

        public ICollection<CrudRowViewModel> CrudRowViewModels { get; set; }
    }
}