using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalWeapon.Monads
{
    public static class MaybeExtensions
    {
        /// <summary>
        /// Filters out all the Nothings, unwrapping the rest to just type <typeparamref name="T"/> and then applies <paramref name="fn"/> to each
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maybes"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectWhereValueExist<T, TResult>(this IEnumerable<Maybe<T>> maybes, Func<T, TResult> fn)
        {
            return from maybe in maybes
                   where maybe.IsSome
                   select fn(maybe.Value);
        }

        /// <summary>
        /// Filters out all the Nothings, unwrapping the rest to just type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybes"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereValueExist<T>(this IEnumerable<Maybe<T>> maybes)
        {
            return SelectWhereValueExist(maybes, m => m);
        }

        /// <summary>
        /// First item or Nothing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Maybe<T> FirstMaybe<T>(this IEnumerable<T> items)
        {
            return Maybe.Apply(items.FirstOrDefault());
        }
    }
}
