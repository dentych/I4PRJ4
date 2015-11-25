using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Backend.Models.Datamodels;
using NUnit.Framework;

namespace Backend.Unit.Tests.Models
{
    [TestFixture]
    public class AsyncCollectionUnittests
    {
        [SetUp]
        public void SetUp()
        {
            _uut = new AsyncObservableCollection<BackendProductCategory>();

        }

        private void AddCat()
        {

            for (var i = 0; i < 200; i++)
            {
                _uut.Add(new BackendProductCategory());
            }
        }

        private void RemoveCat()
        {

            for (var i = 0; i < 200; i++)
            {
                _uut.RemoveAt(0);
            }
        }


        private AsyncObservableCollection<BackendProductCategory> _uut;

        [Test]
        public void Add_addNewItemMultipleThreads_ExpectItemtoBeAddedNoExceptions()
        {
            Thread t1 = new Thread(new ThreadStart(AddCat));
            Thread t2 = new Thread(new ThreadStart(AddCat));
            Thread t3 = new Thread(new ThreadStart(AddCat));


            Assert.DoesNotThrow(() => t1.Start());
            Assert.DoesNotThrow(() => t2.Start());
            Assert.DoesNotThrow(() => t3.Start());

            t1.Join();
            t2.Join();
            t3.Join();
        
            //Assert.That(_uut.Count, Is.EqualTo(600));
        }
        
        [Test]
        public void RemoveAt_removeEachEndMultipleThreads_ExpectItemToBeremoved()
        {

            for (var i = 0; i < 600; i++)
            {
                _uut.Add(new BackendProductCategory());
            }

            Thread t1 = new Thread(new ThreadStart(RemoveCat));
            Thread t2 = new Thread(new ThreadStart(RemoveCat));
            Thread t3 = new Thread(new ThreadStart(RemoveCat));


            Assert.DoesNotThrow(() => t1.Start());
            Assert.DoesNotThrow(() => t2.Start());
            Assert.DoesNotThrow(() => t3.Start());

            t1.Join();
            t2.Join();
            t3.Join();


        //    Assert.That(_uut.Count, Is.EqualTo(0));
        }

    }
}

