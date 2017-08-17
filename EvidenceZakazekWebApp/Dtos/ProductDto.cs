using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderNumber { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public string SupplierName { get; set; }
    }
}