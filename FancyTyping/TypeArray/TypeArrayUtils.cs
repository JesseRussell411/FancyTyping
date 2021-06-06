using System;
using System.Collections.Generic;
using System.Linq;


namespace ExtendedTyping
{
    public static class TypeArrayUtils
    {
        public static IEnumerable<Type> GetParents(this Type self) => new[] { self.BaseType }.Concat(self.GetInterfaces());
        public static IEnumerable<Type> GetSelfAndParents(this Type self) => new[] { self }.Concat(self.GetParents());
    }
}
