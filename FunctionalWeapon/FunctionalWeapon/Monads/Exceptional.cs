﻿using System;
using System.Diagnostics;

namespace FunctionalWeapon.Monads
{
    [DebuggerStepThrough]
    public static class Exceptional
    {
        public static Exceptional<T> Apply<T>(Func<T> func)
        {
            try
            {
                return new Exceptional<T>(func());
            }
            catch (Exception ex)
            {
                return new Exceptional<T>(ex);
            }
        }

        public static Exceptional<T> Apply<T>(T value)
        {
            return new Exceptional<T>(value);
        }

        public static Exceptional<T> Apply<T>(Exception exception)
        {
            return new Exceptional<T>(exception);
        }
    }

    [DebuggerStepThrough]
    public sealed class Exceptional<T>
    {
        readonly T _value;

        public Exceptional(T value)
        {
            _value = value;
        }

        public Exceptional(Exception exception)
        {
            Exception = exception;
            HasException = true;
        }

        public T Value
        {
            get
            {
                if (HasException) throw Exception;

                return _value;
            }
        }

        public bool HasException { get; private set; }

        public Exception Exception { get; private set; }

        public Exceptional<A> Bind<A>(Func<T, A> func)
        {
            try
            {
                return HasException
                    ? new Exceptional<A>(Exception)
                    : new Exceptional<A>(func(_value));
            }
            catch (Exception ex)
            {
                return new Exceptional<A>(ex);
            }
        }

        public Exceptional<A> Bind<A>(Func<T, Exceptional<A>> func)
        {
            try
            {
                return HasException
                    ? new Exceptional<A>(Exception)
                    : func(_value);
            }
            catch (Exception ex)
            {
                return new Exceptional<A>(ex);
            }
        }
    }

    public static class ExceptionalExtensions
    {
        public static Exceptional<T> ToExceptional<T>(this T value)
        {
            return new Exceptional<T>(value);
        }
    }
}
