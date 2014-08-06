using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalWeapon.Monads;

namespace FunctionalWeapon.Tests.MonadsTests
{
    [TestClass]
    public class MaybeTests
    {
        [TestMethod]
        public void ToMaybe_should_not_thrown_exception_on_null_reference()
        {
            string nullStr = null;
            Maybe.Apply(nullStr);
        }

        [TestMethod]
        public void ToMaybe_should_not_throw_exception_on_nullable_value_type()
        {
            int? i = null;
            Maybe.Apply(i);
        }

        [TestMethod]
        public void IsNone_should_return_true_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = Maybe.Apply(nullStr);
            Assert.IsTrue(strMaybe.IsNone);
        }

        [TestMethod]
        public void IsNone_should_return_false_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = Maybe.Apply(str);
            Assert.IsFalse(strMaybe.IsNone);
        }

        [TestMethod]
        public void IsSome_should_return_false_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = Maybe.Apply(nullStr);
            Assert.IsFalse(strMaybe.IsSome);
        }

        [TestMethod]
        public void IsSome_should_return_true_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = Maybe.Apply(str);
            Assert.IsTrue(strMaybe.IsSome);
        }

        [TestMethod]
        public void Bind_should_return_Mybe_with_not_null_for_not_nullable_result()
        {
            string str = "not null";
            var result = Maybe.Apply(str).Bind(s => s.IndexOf("l"));
            Assert.IsTrue(result.IsSome);
        }

        [TestMethod]
        public void Bind_should_return_Maybe_with_null_for_nullable_result()
        {
            string str = "not null";
            var result = Maybe.Apply(str)
                              .Bind(s => s.IndexOf("T"))
                              .Bind(i => i > 0 ? "not null" : null);
            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void Bind_should_return_Maybe_with_null_for_struct_type()
        {
            string str = null;
            var result = Maybe.Apply(str).Bind(s => s.IndexOf("T"));
            Assert.IsTrue(result.IsNone);
        }
    }
}
