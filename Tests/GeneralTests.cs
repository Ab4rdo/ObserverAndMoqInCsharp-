using System;
using Moq;
using NUnit.Framework;
using URLUpdateTestWithMoq;

namespace Tests
{
    public class Tests
    {
        private Mock<IUrlRequester> moqRequester;
        private Mock<UrlMonitor> moqMonitor;
        private Mock<IObserver> moqObserver;

        [SetUp]
        public void SetUp()
        {
            moqRequester = new Mock<IUrlRequester>();
            moqMonitor = new Mock<UrlMonitor>(moqRequester.Object);
            moqObserver = new Mock<IObserver>();
        }
        
        [Test]
        public void SingleNotifyTest()
        {
            //arrange
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            moqMonitor.Object.AddObserver(moqObserver.Object);
            moqMonitor.Object.CheckUrls();
            
            //assert
            moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void TripleNotifyTestWithoutChangeOfDate()
        {
            //arrange
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            moqMonitor.Object.AddObserver(moqObserver.Object);
            moqMonitor.Object.CheckUrls();
            moqMonitor.Object.CheckUrls();
            moqMonitor.Object.CheckUrls();
            
            //assert
            moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void TripleNotifyTestWithChangeOfDate()
        {
            //arrange
            moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            moqMonitor.Object.AddObserver(moqObserver.Object);
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqMonitor.Object.CheckUrls();
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqMonitor.Object.CheckUrls();
            moqMonitor.Object.CheckUrls();
            
            //assert
            moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void TestIfUrlsAreNotRepeating()
        {
            //arrange
            var moqObserver2 = new Mock<IObserver>();
            var exampleUrl = "http://xxxxx.pl";
            moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            moqObserver.SetupProperty(o => o.UrlAddress, exampleUrl);
            moqObserver2.SetupProperty(o => o.UrlAddress, exampleUrl);
            
            //act
            moqMonitor.Object.AddObserver(moqObserver.Object);
            moqMonitor.Object.CheckUrls();
            
            //assert
            moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
            moqObserver2.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Never);
        }
    }
}