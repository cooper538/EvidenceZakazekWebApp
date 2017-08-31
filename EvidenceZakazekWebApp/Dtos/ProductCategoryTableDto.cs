using EvidenceZakazekWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.Dtos
{
    public class ProductCategoryTableDto : ICrudTableDto
    {
        public int Id { get; set; }

        public IDictionary<string, string> StaticProperties { get; set; }

        public IDictionary<string, string> Properties
        {
            get
            {
                return new Dictionary<string, string>(StaticProperties);
            }
        }

        public ICollection<string> ColumnNames
        {
            get
            {
                return Properties.Select(p => p.Key).ToList();
            }
        }
    }
}