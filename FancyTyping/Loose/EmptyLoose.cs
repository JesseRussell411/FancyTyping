using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedTyping
{
    public struct Loose : ILoose
    {
        public Type Type => null;

        public dynamic V => default;

        public object O => null;

        public IEnumerable<Type> WhiteList => new TypeArray();
    }
}
