using System;
using Moq;
using NUnit.Framework;
using URLUpdateTestWithMoq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SingleNotifyTest()
        {
            //arrange
            var moqRequester = new Mock<IUrlRequester>();
            var moqMonitor = new Mock<UrlMonitor>(moqRequester.Object);
            var moqObserver = new Mock<IObserver>();
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            moqMonitor.Object.AddObserver(moqObserver.Object);
            moqMonitor.Object.CheckUrls();
            
            //assert
            moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
        }
    }
}