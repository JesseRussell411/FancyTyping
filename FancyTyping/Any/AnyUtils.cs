using System;

namespace ExtendedTyping
{
    public static class AnyUtils
    {
        internal static string GenerateInvalidCastExceptionMessage(Type self_type, Type to_type, Type from_type)
        {
            return $"Cannot cast the current {self_type} to {to_type} because the value stored is not of the type {to_type}, but is of the type {from_type} instead.";
        }
    }
}
