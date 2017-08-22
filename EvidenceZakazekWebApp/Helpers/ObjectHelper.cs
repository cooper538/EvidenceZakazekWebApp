using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.Helpers
{
    public static class ObjectHelper
    {
        public static IEnumerable<object> GetObjectArray<T>(IEnumerable<T> obj)
        {
            return obj.Select(o => o.GetType().GetProperties().Select(p => p.GetValue(o, null)));
        }
    }
}