using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class DetailViewModel
    {
        public string Heading { get; set; }
        public string ControllerName { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public DetailViewModel()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}