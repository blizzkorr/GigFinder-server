using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Tools
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var x in @this)
            {
                action(x);
                yield return x;
            }
        }
    }
}
