using System;
using System.Diagnostics;

namespace FunctionalWeapon.Monads
{
    [DebuggerStepThrough]
    public struct Maybe
    {
        public static Maybe<T> Apply<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }
    
    [DebuggerStepThrough]
    public struct Maybe<T>
    {
        readonly T _value;
        readonly bool _isSome;

        internal Maybe(T value)
        {
            _isSome = value != null;
            _value = value;
        }

        public bool IsSome { get { return _isSome; } }

        public bool IsNone { get { return !_isSome; } }

        public T Value
        {
            get
            {
                if (_value == null) throw new NullReferenceException("Value");

                return _value;
            }
        }

        public static readonly Maybe<T> None = new Maybe<T>();

        public Maybe<A> Map<A>(Func<T, A> func)
        {
            if (func == null) throw new ArgumentNullException("func");

            return _isSome ? Maybe.Apply(func(_value)) : Maybe<A>.None;
        }

        public Maybe<A> Map<A>(Func<T, Maybe<A>> func)
        {
            if (func == null) throw new ArgumentNullException("func");

            return _isSome ? func(_value) : Maybe<A>.None;
        }

        public A Match<A>(Func<A> none, Func<T, A> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            return _isSome ? some(_value) : none();
        }

        public void Match(Action none, Action<T> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            if (_isSome) some(_value);
            
            else none();
        }
    }
}
