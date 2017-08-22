using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.ViewModels.CustomAttributes
{
    // https://stackoverflow.com/a/5146766/6355668
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count > 0;
            }
            return false;
        }
    }
}