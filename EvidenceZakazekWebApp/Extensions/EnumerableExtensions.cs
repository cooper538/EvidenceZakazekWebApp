﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.Extensions
{
    // https://stackoverflow.com/a/7259419/2756329
    public static class EnumerableExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing<T>(new Random());
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}