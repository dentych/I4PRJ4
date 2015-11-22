using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Events;
using NUnit.Framework;
using Prism.Events;

namespace Backend.Unit.Tests.Models
{
    [TestFixture]
    class EventAggregatorUnitTests
    {
        private IEventAggregator _uut;

        /* Testing of the implementation of Singleton for the even aggregator */

        [Test]
        public void EventAggregator_MakningTwoInstances_ExpectToBeEqual()
        {
            IEventAggregator FirstInstance = SingleEventAggregator.Aggregator;
            _uut = SingleEventAggregator.Aggregator;

            Assert.That(_uut,Is.EqualTo(FirstInstance));
        }

    }
}
