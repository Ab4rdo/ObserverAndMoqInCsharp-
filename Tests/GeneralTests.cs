using System;
using Moq;
using NUnit.Framework;
using URLUpdateTestWithMoq;

namespace Tests
{
    public class Tests
    {
        private Mock<IUrlRequester> _moqRequester;
        private Mock<UrlMonitor> _moqMonitor;
        private Mock<IObserver> _moqObserver;

        [SetUp]
        public void SetUp()
        {
            _moqRequester = new Mock<IUrlRequester>();
            _moqMonitor = new Mock<UrlMonitor>(_moqRequester.Object);
            _moqObserver = new Mock<IObserver>();
        }
        
        [Test]
        public void SingleNotifyTest()
        {
            //arrange
            _moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            _moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            _moqMonitor.Object.AddObserver(_moqObserver.Object);
            _moqMonitor.Object.CheckUrlsLoop(0, 1);
            
            //assert
            _moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void TripleNotifyTestWithoutChangeOfDate()
        {
            //arrange
            _moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            _moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            _moqMonitor.Object.AddObserver(_moqObserver.Object);
            _moqMonitor.Object.CheckUrlsLoop(0, 3);

            
            //assert
            _moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void TripleNotifyTestWithChangeOfDate()
        {
            //arrange
            _moqObserver.SetupProperty(o => o.UrlAddress, "x");
           
            //act
            _moqMonitor.Object.AddObserver(_moqObserver.Object);
            _moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            _moqMonitor.Object.CheckUrls();
            _moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            _moqMonitor.Object.CheckUrls();
            _moqMonitor.Object.CheckUrls();
            
            //assert
            _moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void TestIfUrlsAreNotRepeating()
        {
            //arrange
            var moqObserver2 = new Mock<IObserver>();
            var exampleUrl = "http://xxxxx.pl";
            _moqRequester.Setup(r => r.GetUpdatedDateTimeFromUrl(It.IsAny<string>())).Returns(DateTime.Now);
            _moqObserver.SetupProperty(o => o.UrlAddress, exampleUrl);
            moqObserver2.SetupProperty(o => o.UrlAddress, exampleUrl);
            
            //act
            _moqMonitor.Object.AddObserver(_moqObserver.Object);
            _moqMonitor.Object.CheckUrls();
            
            //assert
            _moqObserver.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Once);
            moqObserver2.Verify(m => 
                m.HandleEvent(It.IsAny<ISubject>(), It.IsAny<string>()), Times.Never);
        }
    }
}