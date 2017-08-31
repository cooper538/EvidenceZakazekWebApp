using EvidenceZakazekWebApp.Dtos.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.Dtos
{
    public class ProductTableDto : ICrudTableDto
    {
        public int Id { get; set; }

        public IDictionary<string, string> StaticProperties { get; set; }
        public IDictionary<string, string> DynamicProperties { get; set; }

        public IDictionary<string, string> Properties {
            get
            {
                return new Dictionary<string, string>(StaticProperties.Union(DynamicProperties).ToDictionary(p => p.Key, p => p.Value));
            }
        }

        public ICollection<string> ColumnNames {
            get
            {
                return Properties.Select(p => p.Key).ToList();
            }
        }
    }
}