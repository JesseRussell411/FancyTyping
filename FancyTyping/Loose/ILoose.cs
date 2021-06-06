using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedTyping
{
    /// <summary>
    /// Allows any item of a type found in the whiteList to be stored.
    /// </summary>
    public interface ILoose : ITyping
    {
        IEnumerable<Type> WhiteList { get; }
    }
}
