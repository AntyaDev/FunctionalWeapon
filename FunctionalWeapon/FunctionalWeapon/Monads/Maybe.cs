using System;
using System.ComponentModel;
using System.Diagnostics;

namespace FunctionalWeapon.Monads
{
    [DebuggerStepThrough]
    public static class Maybe
    {
        public static Maybe<T> Apply<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }

    [DebuggerStepThrough]
    public sealed class Maybe<T>
    {
        readonly T _value;
        readonly bool _isSome;

        public Maybe()
        { }

        public Maybe(T value)
        {
            if (value != null)
            {
                _isSome = true;
                _value = value;
            }            
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsSome { get { return _isSome; } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsNone { get { return !_isSome; } }

        public static readonly Maybe<T> None = new Maybe<T>();

        public T Value
        {
            get
            {
                if (!_isSome) throw new InvalidOperationException("Value is null");

                return _value;
            }
        }
    }

    public static class MaybeExtensions
    {
        public static A Match<T, A>(this Maybe<T> maybe, Func<A> none, Func<T, A> some)
        {
            return maybe == null || maybe.IsNone ? none() : some(maybe.Value);
        }

        public static Maybe<A> Bind<T, A>(this Maybe<T> maybe, Func<T, A> func)
        {
            return func == null || maybe == null || maybe.IsNone
                ? new Maybe<A>()
                : func(maybe.Value).ToMaybe();
        }

        public static Maybe<A> Bind<T, A>(this Maybe<T> maybe, Func<T, Maybe<A>> func)
        {
            return func == null || maybe == null || maybe.IsNone
                ? new Maybe<A>()
                : func(maybe.Value);
        }

        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }
    }
}
