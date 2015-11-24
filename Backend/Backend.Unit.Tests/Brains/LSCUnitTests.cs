using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Communication;
using NUnit.Framework;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class LSCUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            LSC.Connection = null;
            LSC.Listener = null;
        }

        [Test]
        public void Connection_createTwo_ExpectToBeSame()
        {
            var l1 = LSC.Connection;
            var l2 = LSC.Connection;

            Assert.AreEqual(l1,l2);
        }

        [Test]
        public void Listener_createTwo_ExpectToBeSame()
        {
            var l1 = LSC.Listener;
            var l2 = LSC.Listener;

            Assert.AreEqual(l1, l2);
        }

        [Test]
        public void Listener_NullListener_ExpectNotToBeSame()
        {
            var l1 = LSC.Listener;
            LSC.Listener = null;
            var l2 = LSC.Listener;

            Assert.AreNotEqual(l1, l2);
        }

        [Test]
        public void Connection_NullConnection_ExpectNotToBeSame()
        {
            var l1 = LSC.Listener;
            LSC.Listener = null;
            var l2 = LSC.Listener;

            Assert.AreNotEqual(l1, l2);
        }

        [Test]
        public void Connection_NullConnection_ExpectNotToBeNull()
        {
            LSC.Connection = null;
            Assert.NotNull(LSC.Connection);
        }

        [Test]
        public void Listener_NullListener_ExpectNotToBeNull()
        {
            LSC.Listener = null;
            Assert.NotNull(LSC.Listener);
        }

        [Test]
        public void Listener_NullConnection_ExpectNewConnection()
        {

            var c1 = LSC.Connection;
            LSC.Connection = null;

            var l1 = LSC.Listener;
            var c2 = LSC.Connection;

            Assert.AreNotEqual(c1,c2);

        }


    }
}
