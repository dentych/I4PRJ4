using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest
{
    [TestFixture]
    class CommandUnitTest
    {
        GetCatalogueCmd cmd;

        [Test]
        public void CmdName_StripsCmdPostfix()
        {
            cmd = Substitute.For<GetCatalogueCmd>();

            Assert.That(cmd.CmdName.Equals("GetCatalogue"));
        }
    }
}
