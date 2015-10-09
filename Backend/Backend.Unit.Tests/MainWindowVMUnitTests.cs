using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;
using static NSubstitute.Substitute;

namespace Backend.Unit.Tests
{
    [TestFixture]
    class MainWindowVmUnitTests
    {

        private MainWindowViewModel _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new MainWindowViewModel();
        }

        /* 
         * Lortet åbner rent faktisk vinduet, og man skal trykke på Anuller...
        [Test]
        [RequiresSTAAttribute]
        public void OpenAddProductWindowCommand_Execute_ExpectCallToNWD()
        {
            _uut.OpenAddProductWindowCommand.Execute(null);
            Assert.True(_uut.IsCalled);
        }
        */ 
    }
}
