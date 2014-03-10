using System;

namespace FunctionalWeapon.Monads
{
    public sealed class Maybe<T>
    {
        readonly T _value;

        public Maybe(T value)
        {
            _value = value;
        }

        public bool IsSome { get { return _value != null; } }

        public bool IsNone { get { return _value == null; } }

        public T Value { get { return _value; } }

        public Maybe<A> Bind<A>(Func<T, A> func)
        {
            return IsSome ? func(_value).ToMaybe() : new Maybe<A>(default(A));
        }
    }

    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> ToMaybe<T>(this T? value) where T : struct
        {
            return new Maybe<T>(value.Value);
        }
    }
}
