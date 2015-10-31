using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol;


namespace SharedLib.UnitTest
{
    [TestFixture]
    public class XmlBufferUnitTest
    {
        [Test]
        public void Encode_MarshalEncodeCalled()
        {
            var s = "<Command Name=\"Demo\" />";
            var buffer = new XmlBuffer();
            var docs = buffer.GetDocuments();
            docs.ToList();

            buffer.AddData(s);

            //Assert.That(docs.First, Is.EqualTo(s));
        }
    }
}
