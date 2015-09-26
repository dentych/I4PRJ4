using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.OpretProdukt.Unit.Test
{
    [TestFixture]
    public class OpretProduktTests
    {

        [Test]
        //[ExpectedException(typeof(Exception))]
        public void justattest()
        {
            var factory = new ManagerFactory();
            var ProjectManger = factory.MakeProductManager("Project");
            ProjectManger.AddProduct(new Dictionary<string, string>
            {
                {"Name", "Bananer"},
                {"ProductNumber", "191919191"},
                {"Price", "125"}
            });

            var data = ProjectManger.myProduct.GetData();

            Assert.That(data["Name"], Is.EqualTo("Bananer"));
            Assert.That(data["ProductNumber"], Is.EqualTo("191919191"));
            Assert.That(data["Price"], Is.EqualTo("125"));

        }
    }
}
