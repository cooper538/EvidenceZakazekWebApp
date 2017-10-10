using EvidenceZakazekWebApp.Core.Models;
using System;
using System.Collections.Generic;

namespace EvidenceZakazekWebApp.Core
{
    //https://stackoverflow.com/a/23581597/2756329
    // Metoda/ property ComparableId
    public class PropertyDefinitionEqualityComparer : IEqualityComparer<PropertyDefinition>
    {
        public bool Equals(PropertyDefinition x, PropertyDefinition y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            return x.Id == y.Id;
        }

        public int GetHashCode(PropertyDefinition model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return model.Id.GetHashCode();
        }
    }
}