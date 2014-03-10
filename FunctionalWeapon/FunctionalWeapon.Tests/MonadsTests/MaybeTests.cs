using System;
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
            nullStr.ToMaybe();
        }

        [TestMethod]
        public void ToMaybe_should_not_throw_exception_on_nullable_value_type()
        {
            int? i = null;
            i.ToMaybe();
        }

        [TestMethod]
        public void IsNone_should_return_true_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = nullStr.ToMaybe();
            Assert.IsTrue(strMaybe.IsNone);
        }

        [TestMethod]
        public void IsNone_should_return_false_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = str.ToMaybe();
            Assert.IsFalse(strMaybe.IsNone);
        }

        [TestMethod]
        public void IsSome_should_return_false_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = nullStr.ToMaybe();
            Assert.IsFalse(strMaybe.IsSome);
        }

        [TestMethod]
        public void IsSome_should_return_true_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = str.ToMaybe();
            Assert.IsTrue(strMaybe.IsSome);
        }

        [TestMethod]
        public void Bind_should_return_Mybe_with_not_null_for_not_nullable_result()
        {
            string str = "not null";
            var result = str.ToMaybe().Bind(s => s.IndexOf("l"));
            Assert.IsTrue(result.IsSome);
        }

        [TestMethod]
        public void Bind_should_return_Maybe_with_null_for_nullable_result()
        {
            string str = "not null";
            var result = str.ToMaybe()
                            .Bind(s => s.IndexOf("T"))
                            .Bind(i => i > 0 ? "not null" : null);
            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void Bind_should_return_Maybe_with_null_for_struct_type()
        {
            string str = null;
            var result = str.ToMaybe().Bind(s => s.IndexOf("T"));
            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Value_should_thorwn_exception_for_null_value()
        {
            string str = null;
            var value = str.ToMaybe().Value;
        }
    }
}
