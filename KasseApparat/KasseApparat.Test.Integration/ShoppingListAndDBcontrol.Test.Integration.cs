using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace KasseApparat.Test.Integration
{
    [TestFixture]
    class ShoppingListAndDBcontrol
    {
        IDBcontrol sut = null;

        [SetUp]
        public void SetUp()
        {
            sut = Substitute.For<IDBcontrol>();
        }
    }
}
