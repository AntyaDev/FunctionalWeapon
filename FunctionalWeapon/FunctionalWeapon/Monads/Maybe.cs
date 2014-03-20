using System;
using System.Diagnostics;
using System.Reflection;

namespace FunctionalWeapon.Monads
{
    [DebuggerStepThrough]
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value)
        {
            return value.ToMaybe();
        }

        public static Maybe<T> None<T>()
        {
            return new Maybe<T>();
        }
    }

    [DebuggerStepThrough]
    public sealed class Maybe<T>
    {
        readonly T _value;
        
        public Maybe(T value)
        {
            if (value != null)
            {
                IsSome = true;
                _value = value;
            }
            else IsNone = true;
        }
        
        public Maybe()
        {
            IsNone = true;
        }

        public bool IsSome { get; private set; }

        public bool IsNone { get; private set; }

        public T Value
        {
            get
            {
                if (IsNone) throw new NullReferenceException("Value is null");

                return _value;
            }
        }

        public Maybe<A> Bind<A>(Func<T, A> func)
        {
            if (func == null) return new Maybe<A>();

            return IsSome ? func(_value).ToMaybe() : new Maybe<A>();
        }

        public Maybe<A> Bind<A>(Func<T, Maybe<A>> func)
        {
            if (func == null) return new Maybe<A>();

            return func(_value);
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
            if (value == null)
            {
                var constructor = typeof(Maybe<T>).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, 
                                                                  null, new Type[0], null);
                return (Maybe<T>)constructor.Invoke(null);
            }
            return new Maybe<T>(value.Value);
        }
    }
}
