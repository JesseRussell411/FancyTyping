using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendedTyping
{
    public struct TypeArray : ITypeArray
    {
        public IEnumerator<Type> GetEnumerator() => Enumerable.Empty<Type>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public Type this[int i] => throw new IndexOutOfRangeException();

        public int Length => 0;

        public static implicit operator Type[](TypeArray ta) => new Type[0];
    }
}
