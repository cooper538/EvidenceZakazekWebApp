using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class CrudTableViewModel
    {
        public string Heading { get; set; }

        public IEnumerable<CrudRowViewModel> CrudRowViewModels { get; set; }
        public IEnumerable<string> ColumnNames => CrudRowViewModels.FirstOrDefault()?.ColumnNames;
    }
}