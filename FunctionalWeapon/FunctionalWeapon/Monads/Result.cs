using System;
using System.Diagnostics;

namespace FunctionalWeapon.Monads
{
    [DebuggerStepThrough]
    public class Result
    {
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result<T> Fail<T>(Exception error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result<T> Apply<T>(Func<T> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception ex)
            {
                return Fail<T>(ex);
            }
        }
    }

    [DebuggerStepThrough]
    public class Result<T>
    {
        readonly T _value;
        readonly bool _isSuccess;
        readonly Exception _error;

        internal Result(T value, bool isSuccess, Exception error)
        {
            _value = value;
            _isSuccess = isSuccess;
            _error = error;
        }

        public bool IsSuccess { get { return _isSuccess; } }

        public Exception Error { get { return _error; } }

        public T Value { get { return _value; } }

        public bool IsFailure
        {
            get { return !IsSuccess; }
        }

        public Result<A> OnOk<A>(Func<T, Result<A>> func)
        {
            try
            {
                return _isSuccess ? func(_value) : Result.Fail<A>(_error);
            }
            catch (Exception ex)
            {
                return Result.Fail<A>(ex);
            }
        }

        public Result<A> OnOk<A>(Func<T, A> func)
        {
            try
            {
                return _isSuccess ? Result.Ok(func(_value)) : Result.Fail<A>(_error);
            }
            catch (Exception ex)
            {
                return Result.Fail<A>(ex);
            }
        }
    }
}
