using EvidenceZakazekWebApp.Helpers.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EvidenceZakazekWebApp.Helpers
{
    public static class ObjectHelper
    {
        [Obsolete("Pointlessly incerase complixity", true)]
        public static IEnumerable<object> GetObjectArray<T>(IEnumerable<T> obj)
        {
            return obj.Select(o => o.GetType().GetProperties().Select(p => p.GetValue(o, null)));
        }

        [Obsolete("Pointlessly incerase complixity", true)]
        // hint https://stackoverflow.com/a/6637710/6355668
        public static Dictionary<string, string> NonGenericPropertiesToDictionary(this object obj)
        {
            return obj.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => (p.GetCustomAttribute(typeof(IgnoreAttribute), true) == null) && !(p.PropertyType.IsGenericType))
                           .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null).ToString());

        }
    }
}