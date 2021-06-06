using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace JesseRussell.FancyTyping
{
    public struct Any : IAny
    {
        /// <summary>
        /// The value stored in the current Any type.
        /// </summary>
        public dynamic Value => default;
        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();
        /// <summary>
        /// The type of the value stored in the current Any type.
        /// </summary>
        public Type Type => null;
        /// <summary>
        /// TypeArray containing all of the types that are allowed.
        /// </summary>
        public static readonly IEnumerable<Type> whiteList = new TypeArray();
        /// <summary>
        /// TypeArray containing all of the types that are allowed.
        /// </summary>
        public IEnumerable<Type> WhiteList => whiteList;
        /// <summary>
        /// Returns a set containing all of the types that are allowed.
        /// </summary>
        public static readonly ImmutableHashSet<Type> whiteSet = new HashSet<Type>(whiteList).ToImmutableHashSet();
        /// <summary>
        /// Returns a set containing all of the types that are allowed.
        /// </summary>
        public ImmutableHashSet<Type> WhiteSet => whiteSet;
        public static bool operator ==(Any left, Any right) => true;
        public static bool operator !=(Any left, Any right) => true;
    }
}
