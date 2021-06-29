﻿# Author: Jesse Russell
# Summary: Generates the struct definitions for the Any datatype, up to n type parameters.

import sys

# Parameters:
FILE_NAME = "Any.cs"
NAMESPACE = "JesseRussell.FancyTyping"
UTILS_NAME = "AnyUtils"
STRUCT_NAME = "Any"
INTERFACE_NAME = "IAny"
VALUE_TYPE = "object"
VALUE_NAME = "Value"
TYPE_PARAMETER_COUNT = 8
#


# Constants:
INDENT = "    "
#


# Prep VALUE_NAME. un-capitalize it.
VALUE_NAME = VALUE_NAME[0].lower() + VALUE_NAME[1:]


# Returns string representation of the parameter: enumerable, delimited by the delim parameter.
def to_delim_string(enumerable, delim=", "):
    s = ""
    first = True
    for item in enumerable:
        if not first:
            s = s + delim

        s = s + str(item)

        first = False

    return s


# Open output file.
output = open(FILE_NAME, "w")
# writeline, appends linebreak automatically
def outwriteline(s):
    output.write(s + "\n")

# tn...
def t(j):
    return "t" + str(j)

# Tn...
def T(j):
    return "T" + str(j)

# #=========================#
# | Begin code generation:  |
# | ----------------------  |
# #=========================#

# Note.
outwriteline("//This code was generated by " + sys.argv[0])

# Using directives:
outwriteline("using System;")
outwriteline("using System.Linq;")
outwriteline("using System.Collections.Immutable;")
#

# Namespace declaration.
outwriteline("namespace " + NAMESPACE)
outwriteline("{");

types = []
# Main loop...
for i in range(1, TYPE_PARAMETER_COUNT + 1):
    # Increment the last type parameter.
    types.append(T(i));

    FULL_NAME = STRUCT_NAME + "<" + to_delim_string(types) + ">";

    # struct doc:
    outwriteline(INDENT + "/// <summary>")
    outwriteline(INDENT + "/// Can store any value of a type found in the type arguments.")
    outwriteline(INDENT + "/// </summary>")


    # Struct declaration:
    outwriteline(INDENT + "public struct " + FULL_NAME + " : " + INTERFACE_NAME)
    outwriteline(INDENT + "{")
    #

    # properties:
    outwriteline(INDENT * 2 + "public object Value { get; }")
    outwriteline(INDENT * 2 + "public Type Type => Value.GetType();")

    # whiteList
    outwriteline(INDENT * 2 + "private static ImmutableHashSet<Type> _whiteList = null;")
    outwriteline(INDENT * 2 + "public static ImmutableHashSet<Type> WhiteList => _whiteList ??= new TypeArray<" + to_delim_string(types) + ">().ToImmutableHashSet();")

    # constructors:
    for j in range(1, i + 1):
        outwriteline(INDENT * 2 + "public " + STRUCT_NAME + "(" + T(j) + " value) => Value = value;")

    # Implicit casts...
    for j in range(1, i + 1):
        outwriteline(INDENT * 2 + "public static implicit operator " + FULL_NAME + "(" + T(j) + " value) => new " + FULL_NAME + "(value);")


    # Explicit casts...
    for j in range(1, i + 1):
        outwriteline(INDENT * 2 + "public static explicit operator " + T(j) + "(" + FULL_NAME + " self) => self.TryMatch(out " + T(j) + " result) ? result : throw new InvalidCastException();")

    #Obligatory object stuff
    outwriteline(INDENT * 2 + "public override string ToString() => Value?.ToString();")
    outwriteline(INDENT * 2 + "public override int GetHashCode() => Value?.GetHashCode() ?? HashCode.Combine((object)null);")
    outwriteline(INDENT * 2 + "public override bool Equals(object obj) => Value?.Equals(obj) ?? obj?.Equals(Value) ?? true;")
    # Close struct.
    output.write("    }\n")

# Close namespace.
output.write("}")

# *Code Generation finished.

# Close output file.
output.close()
