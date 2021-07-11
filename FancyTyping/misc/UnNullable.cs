using System;
using System.Collections.Generic;
using System.Text;

namespace FancyTyping.misc
{
    // Like nullable which makes un-nullable types nullable. This makes nullable types un-nullable.
    public readonly struct UnNullable<T>
    {
        public readonly T Value;
        public UnNullable(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Value = value;
            }
        }
        public UnNullable(UnNullable<T> value) => Value = value.Value;

        public static implicit operator T(UnNullable<T> unNullable) => unNullable.Value;
        public static implicit operator UnNullable<T>(T value) => new UnNullable<T>(value);
        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();
    }
}
