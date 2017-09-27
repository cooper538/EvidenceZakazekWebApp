using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.ViewModels.Partial
{
    public class CrudRowViewModel
    {
        public int Id { get; set; }

        public IDictionary<string, string> Properties { get; set; }

        public IEnumerable<string> ColumnNames
        {
            get
            {
                return Properties.Select(p => p.Key).ToList();
            }
        }

        public CrudRowViewModel()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}