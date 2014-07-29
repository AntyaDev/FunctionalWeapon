using System;
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

        Maybe() { }

        public Maybe(T value)
        {
            if (value != null)
            {
                _isSome = true;
                _value = value;
            }            
        }
        
        public bool IsSome { get { return _isSome; } }

        public bool IsNone { get { return !_isSome; } }

        public static readonly Maybe<T> None = new Maybe<T>();
        
        internal T Value
        {
            get
            {
                if (!_isSome) throw new InvalidOperationException("Value is null");

                return _value;
            }
        }
    }

    [DebuggerStepThrough]
    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<A> Bind<T, A>(this Maybe<T> maybe, Func<T, A> func)
        {
            return func == null || maybe == null || maybe.IsNone
                ? Maybe<A>.None
                : func(maybe.Value).ToMaybe();
        }

        public static Maybe<A> Bind<T, A>(this Maybe<T> maybe, Func<T, Maybe<A>> func)
        {
            return func == null || maybe == null || maybe.IsNone
                ? Maybe<A>.None
                : func(maybe.Value);
        }

        public static A Match<T, A>(this Maybe<T> maybe, Func<A> none, Func<T, A> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            return maybe == null || maybe.IsNone ? none() : some(maybe.Value);
        }

        public static void Match<T>(this Maybe<T> maybe, Action none, Action<T> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            if (maybe == null || maybe.IsNone) none();
            else some(maybe.Value);
        }
    }
}
