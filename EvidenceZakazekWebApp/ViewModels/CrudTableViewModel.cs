using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class CrudTableViewModel
    {
        public string Heading { get; set; }
        public string ControllerName { get; set; }

        public ICollection<CrudRowViewModel> CrudRowViewModels { get; set; }

        public CrudTableViewModel()
        {
            CrudRowViewModels = new Collection<CrudRowViewModel>();
        }
    }
}