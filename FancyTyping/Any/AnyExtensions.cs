using System;
using System.Collections.Generic;
using System.Text;

namespace JesseRussell.FancyTyping
{
    public static class AnyExtensions
    {
        public static T Match<T>(this IAny self) => self.Value is T t ? t : default;
        public static bool TryMatch<T>(this IAny self) => self.Value is T;
        public static bool TryMatch<T>(this IAny self, out T value)
        {
            if (self.Value is T t)
            {
                value = t;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public static O Match<T, O>(this IAny self, Func<T, O> func)
        {
            return self.TryMatch(out T value) ? func(value) : default;
        }
        public static O Match<T, O>(this IAny self, Func<T, O> func, O fallbackValue)
        {
            return self.TryMatch(out T value) ? func(value) : fallbackValue;
        }
        public static O Match<T, O>(this IAny self, Func<T, O> func, Func<IAny, O> fallback)
        {
            return self.TryMatch(out T value) ? func(value) : fallback(self);
        }

        public static IAny Match<T>(this IAny self, Action<T> action)
        {
            if (self.TryMatch(out T value))
            {
                action(value);
            }
            return self;
        }
        public static IAny Match<T>(this IAny self, Action<T> action, Action<IAny> fallback)
        {
            if (self.TryMatch(out T value))
            {
                action(value);
            }
            else
            {
                fallback(self);
            }
            return self;
        }

        public static bool TryMatch<T, O>(this IAny self, Func<T, O> func, out O result)
        {
            if (self.TryMatch(out T value))
            {
                result = func(value);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
        public static bool TryMatch<T, O>(this IAny self, Func<T, O> func, O fallbackValue, out O result)
        {
            if (self.TryMatch(out T value))
            {
                result = func(value);
                return true;
            }
            else
            {
                result = fallbackValue;
                return false;
            }
        }
        public static bool TryMatch<T, O>(this IAny self, Func<T, O> func, Func<IAny, O> fallback, out O result)
        {
            if (self.TryMatch(out T value))
            {
                result = func(value);
                return true;
            }
            else
            {
                result = fallback(self);
                return false;
            }
        }

        public static bool TryMatch<T>(this IAny self, Action<T> action)
        {
            if (self.TryMatch(out T value))
            {
                action(value);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool TryMatch<T>(this IAny self, Action<T> action, Action<IAny> fallback)
        {
            if (self.TryMatch(out T value))
            {
                action(value);
                return true;
            }
            else
            {
                fallback(self);
                return false;
            }
        }
    }
}
