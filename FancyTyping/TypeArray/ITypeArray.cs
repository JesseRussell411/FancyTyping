using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedTyping
{
    /// <summary>
    /// Represents an indexable array of Type.
    /// </summary>
    public interface ITypeArray : IEnumerable<Type>
    {
        Type this[int i] { get; }
        int Length { get; }
    }
}
