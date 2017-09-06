namespace EthereumSamurai.EventProvider.Core.Tests
{
    using Api.Models.Validation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EthereumAddressAttributeTest
    {
        private readonly EthereumAddressAttribute _validator;

        public EthereumAddressAttributeTest()
        {
            _validator = new EthereumAddressAttribute();
        }


        [TestMethod]
        public void IsValid__ValueIsValidString__ReturnsTrue()
        {
            var result = _validator.IsValid("0x1663fcb2f6566723a4c429f8ed34352726680f9a");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid__ValueIsInvalidString__ReturnsFalse()
        {
            var result = _validator.IsValid("0x1663");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid__ValueIsNotString__ReturnsFalse()
        {
            var result = _validator.IsValid(new object());

            Assert.IsFalse(result);
        }
    }
}