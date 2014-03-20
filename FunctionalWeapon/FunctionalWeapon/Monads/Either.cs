using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalWeapon.Monads
{
    public sealed class Either<T>
    {
        readonly T _value;

        public Either(T value)
        {
            _value = value;
        }

        Either()
        { }

        public bool IsValid { get; private set; }

        public bool IsNotValid { get; private set; }

        public Exception Exception { get; private set; }

        public Either<A> Bind<A>(Func<T, A> func)
        {
            try
            {
                return new Either<A>(func(_value));
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }
    }
}
