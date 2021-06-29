﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace JesseRussell.FancyTyping
{
    /// <summary>
    /// Allows any item of a type found in the whiteList to be stored.
    /// </summary>
    public interface IAny
    {
        // requirements:
        object Value { get; }
        Type Type { get; }

        // self implemented:
        public static ImmutableHashSet<Type> WhiteList = new TypeArray().ToImmutableHashSet();

    }
}
