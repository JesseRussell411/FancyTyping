using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ExtendedTyping
{
    /// <summary>
    /// Allows any item of a type found in the whiteList to be stored.
    /// </summary>
    public interface IAny
    {
        IEnumerable<Type> WhiteList { get; }
        ImmutableHashSet<Type> WhiteSet { get; }

    }
}
