using System;
using NUnit.Framework;
using URLUpdateTestWithMoq;

namespace Tests
{
    public class UrlHolderMementoTests
    {
        private UrlHolder _urlHolder;
        private MementoCaretaker<UrlHolderMemento> _caretaker;
        private DateTime _exampleDate;
        private string _exampleUrl;
        
        [SetUp]
        public void SetUp()
        {
            _urlHolder = new UrlHolder();
            _caretaker = new MementoCaretaker<UrlHolderMemento>();
            _exampleDate = DateTime.Now;
            _exampleUrl = "http://test.pl";
        }
        
        [Test]
        public void TestUrlHolderMementoCreateFromObject()
        {
            //act
            _urlHolder.Put(_exampleUrl, _exampleDate);
            _caretaker.Memento = _urlHolder.CreateMemento();
            
            //assert
            Assert.AreEqual(1, _urlHolder.GetList().Count);
            Assert.AreEqual(1, _caretaker.Memento.UrlsWithDates.Count);
            Assert.AreEqual(_exampleDate, _caretaker.Memento.UrlsWithDates[_exampleUrl]);
            Assert.AreEqual(_caretaker.Memento.UrlsWithDates[_exampleUrl],
                            _urlHolder.GetList().Find(u => u.Key == _exampleUrl).Value);
            Assert.AreEqual(_exampleUrl,
                            _urlHolder.GetList().Find(u => u.Key == _exampleUrl).Key);
        }

        [Test]
        public void TestUrlHolderMementoCreateFromFile()
        {
            //arrange
            var caretakerWithFile = new MementoCaretaker<UrlHolderMemento>();
            
            //act
            _urlHolder.Put(_exampleUrl, _exampleDate);
            _caretaker.Memento = _urlHolder.CreateMemento();
            caretakerWithFile.Memento = new UrlHolderMemento(UrlHolderMemento.ExportFileName);
            
            //assert
            Assert.AreEqual(_caretaker.Memento.UrlsWithDates.Count,
                            caretakerWithFile.Memento.UrlsWithDates.Count);
            Assert.AreEqual(_caretaker.Memento.UrlsWithDates[_exampleUrl],
                            caretakerWithFile.Memento.UrlsWithDates[_exampleUrl]);
        }

        [Test]
        public void TestUrlHolderRestoringFromMemento()
        {         
            //act
            _urlHolder.Put(_exampleUrl, _exampleDate);
            _caretaker.Memento = _urlHolder.CreateMemento();
            _urlHolder.Put(_exampleUrl, DateTime.MinValue);
            _urlHolder.SetMemento(_caretaker.Memento);
            
            //assert
            Assert.AreEqual(_urlHolder.GetList().Find(u => u.Key == _exampleUrl).Value,
                _caretaker.Memento.UrlsWithDates[_exampleUrl]);
        }

        [Test]
        public void TestUrlHolderChangingComparedToMemento()
        {
            //act
            _urlHolder.Put(_exampleUrl, _exampleDate);
            _caretaker.Memento = _urlHolder.CreateMemento();
            _urlHolder.Put(_exampleUrl, DateTime.MinValue);
            
            //assert
            Assert.AreNotEqual(_urlHolder.GetList().Find(u => u.Key == _exampleUrl).Value,
                _caretaker.Memento.UrlsWithDates[_exampleUrl]);
        }
    }
}