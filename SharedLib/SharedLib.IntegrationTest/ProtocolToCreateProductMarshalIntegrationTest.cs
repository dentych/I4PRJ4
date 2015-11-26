using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands;

namespace SharedLib.IntegrationTest
{
    [TestFixture]
    public class ProtocolToCreateProductMarshalIntegrationTest
    {
        Protocol.Protocol protocol;
        Product product;
        CreateProductCmd cmd;
        CreateProductMarshal cpMarshal;
        string data;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 1,
                ProductNumber = "20",
                ProductCategoryId = 5

            };
            cmd = Substitute.For<CreateProductCmd>(product);
            cpMarshal = Substitute.For<CreateProductMarshal>();
            protocol = new Protocol.Protocol();
            data = cpMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            protocol = null;
            product = null;
            cmd = null;
            cpMarshal = null;
            data = null;
        }


        [Test]
        public void Protocol_CreateProductMarshal_EncodeCalled()
        {
            protocol.Encode(cmd);
            cpMarshal.Received(1).Encode(cmd);
        }
    }
}
