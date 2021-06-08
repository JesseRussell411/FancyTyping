﻿# Author: Jesse Russell
# Summary: Generates the struct definitions for the Any datatype, up to n type parameters.

import sys

# Parameters:
FILE_NAME = "Any.cs"
NAMESPACE = "JesseRussell.FancyTyping"
UTILS_NAME = "AnyUtils"
STRUCT_NAME = "Any"
INTERFACE_NAME = "IAny"
VALUE_TYPE = "dynamic"
VALUE_NAME = "Value"
TYPE_PARAMETER_COUNT = 32
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

# #=========================#
# | Begin code generation:  |
# | ----------------------  |
# #=========================#

# Note.
output.write("//This code was generated by " + sys.argv[0] + "\n")

# Using directives:
output.write("using System;\n")
output.write("using System.Collections.Generic;\n")
output.write("using System.Collections.Immutable;\n")
#

# Namespace declaration.
output.write("namespace " + NAMESPACE + "\n{\n")


# tn...
def t(j):
    return "t" + str(j)

# Tn...
def T(j):
    return "T" + str(j)

types = []
# Main loop...
for i in range(1, TYPE_PARAMETER_COUNT + 1):
    # Increment the last type parameter.
    types.append(T(i));

    FULL_NAME = STRUCT_NAME + "<" + to_delim_string(types) + ">";

    # Struct declaration:
    output.write("    public struct " + STRUCT_NAME + "<")

    output.write(to_delim_string(types))

    output.write("> : " + INTERFACE_NAME + "\n")
    output.write("    {\n")
    #

    # Implicit casts...
    for j in range(1, i + 1):
        output.write("        public static implicit operator " + FULL_NAME + "(")
        output.write(T(j) + " " + t(j) + ") => new " + FULL_NAME + "((object)" + t(j) + ");\n")

    # Explicit casts...
    for j in range(1, i + 1):
        output.write("        public static explicit operator " + T(j) + "(" + FULL_NAME + " self) => self.")
        output.write(VALUE_NAME.capitalize() + " is " + T(j) + " " + t(j) + " ? " + t(j) + " : ")

            # Invalid cast exception.
        output.write("throw new InvalidCastException(")
        output.write(UTILS_NAME + ".GenerateInvalidCastExceptionMessage(self.GetType(), typeof(" + T(j) + ")")
        output.write(", self." + VALUE_NAME.capitalize() + ".GetType()));\n")

    # Public constructor
    output.write(INDENT * 2 + "public " + STRUCT_NAME + "(" + FULL_NAME + " " + VALUE_NAME + ") => " + VALUE_NAME.capitalize() + " = " + VALUE_NAME + "." + VALUE_NAME.capitalize() + ";\n")

    # Remaining members:
    # ------------------

    # Value field:
    output.write(
        "        /// <summary>\n" +
        "        /// The value stored in the current " + STRUCT_NAME + " type.\n" +
        "        /// </summary>\n")
    output.write("        public readonly " + VALUE_TYPE + " " + VALUE_NAME.capitalize() + ";\n")
    #

    # private Constructor.
    output.write("        private " + STRUCT_NAME + "(" + VALUE_TYPE + " " + VALUE_NAME + ") => " + VALUE_NAME.capitalize() + " = " + VALUE_NAME + ";\n")


    # Object Method Overrides:
    output.write("        public override string ToString() => " + VALUE_NAME.capitalize() + ".ToString();\n")
    output.write("        public override bool Equals(object obj) => " + VALUE_NAME.capitalize() + ".Equals(obj);\n")
    output.write("        public override int GetHashCode() => " + VALUE_NAME.capitalize() + ".GetHashCode();\n")
    #

    # Derived Properties:
        # Type:
    output.write(
        "        /// <summary>\n" +
        "        /// The type of the value stored in the current " + STRUCT_NAME + " type.\n" +
        "        /// </summary>\n")
    output.write("        public Type Type => " + VALUE_NAME.capitalize() + ".GetType();\n")
        #

        # WhiteList
    output.write(
        INDENT * 2 + "/// <summary>\n" +
        INDENT * 2 + "/// TypeArray containing all of the types that are allowed.\n" +
        INDENT * 2 + "/// </summary>\n"
    )
    output.write(INDENT * 2 + "public static readonly IEnumerable<Type> whiteList = new TypeArray<" + to_delim_string(types) + ">();\n")

    output.write(
        INDENT * 2 + "/// <summary>\n" +
        INDENT * 2 + "/// TypeArray containing all of the types that are allowed.\n" +
        INDENT * 2 + "/// </summary>\n"
    )
    output.write(INDENT * 2 + "public IEnumerable<Type> WhiteList => whiteList;\n")
        #

        # WhiteSet
    output.write(
        INDENT * 2 + "/// <summary>\n" +
        INDENT * 2 + "/// Returns a set containing all of the types that are allowed.\n" +
        INDENT * 2 + "/// </summary>\n"
    )
    output.write(INDENT * 2 + "public static readonly ImmutableHashSet<Type> whiteSet = new HashSet<Type>(whiteList).ToImmutableHashSet();\n")

    output.write(
        INDENT * 2 + "/// <summary>\n" +
        INDENT * 2 + "/// Returns a set containing all of the types that are allowed.\n" +
        INDENT * 2 + "/// </summary>\n"
    )
    output.write(INDENT * 2 + "public ImmutableHashSet<Type> WhiteSet => whiteSet;\n")
        #
    #

    # Operators
        # ==
    output.write(INDENT * 2 + "public static bool operator ==(" + FULL_NAME + " left, " + FULL_NAME + " right) => left.")
    output.write(VALUE_NAME.capitalize() + ".Equals(right." + VALUE_NAME.capitalize() + ");\n")

        # !=
    output.write(INDENT * 2 + "public static bool operator !=(" + FULL_NAME + " left, " + FULL_NAME + " right) => !left.")
    output.write(VALUE_NAME.capitalize() + ".Equals(right." + VALUE_NAME.capitalize() + ");\n")
    #

    # match
    output.write(INDENT * 2 + "public R Match<R, T>(Func<T, R> action, R returnFallback = default)\n")
    output.write(INDENT * 2 + "{\n")
    output.write(INDENT * 3 + "if (" + VALUE_NAME.capitalize() + " is T t) { return action(t); }\n")
    output.write(INDENT * 3 + "else { return returnFallback; }\n")
    output.write(INDENT * 2 + "}\n")
    output.write(INDENT * 2 + "public bool Match<T>(Action action)\n")
    output.write(INDENT * 2 + "{\n")
    output.write(INDENT * 3 + "if (" + VALUE_NAME.capitalize() + " is T t)\n")
    output.write(INDENT * 3 + "{\n")
    output.write(INDENT * 4 + "action<T>(t);\n")
    output.write(INDENT * 4 + "return true;\n")
    output.write(INDENT * 3 + "} else { return false; }\n")
    output.write(INDENT * 2 + "}\n")
    
    # Close struct.
    output.write("    }\n")

# Close namespace.
output.write("}")

# *Code Generation finished.

# Close output file.
output.close()
