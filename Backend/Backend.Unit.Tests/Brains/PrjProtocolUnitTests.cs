using Backend.Brains;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class PrjProtocolUnitTests
    {
        [Test]
        public void ProductXMLParser_RealProdcuct_ExpectCorrectString()
        {
            var testProduct = new Product();
            testProduct.Name = "Test";
            testProduct.Price = 10;
            testProduct.ProductNumber = "ABC123";

            var uut = new PrjProtokol();

            Assert.That(uut.ProductXMLParser(testProduct),
                Is.EqualTo(
                    "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"CreateProduct\"><Product Name=\"Test\" ProductNumber=\"ABC123\" Price=\"10\" /></Command>"));
        }
    }
}