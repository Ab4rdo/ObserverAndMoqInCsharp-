using System;
using System.Collections.Generic;
using System.Linq;

namespace URLUpdateTestWithMoq
{    
    public class UrlHolder
    {
        private Dictionary<string, DateTime> _urlsWithDates = new Dictionary<string, DateTime>();

        public void Put(string url, DateTime date)
        {
            if (_urlsWithDates.ContainsKey(url))
                _urlsWithDates[url] = date;
            else
                _urlsWithDates.Add(url, date);
        }

        public void Remove(string url)
        {
            _urlsWithDates.Remove(url);
        }

        public List<KeyValuePair<string, DateTime>> GetList()
        {
            return _urlsWithDates.ToList();
        }

        public void SetMemento(UrlHolderMemento memento)
        {
            _urlsWithDates = new Dictionary<string, DateTime>(memento.UrlsWithDates);
        }

        public UrlHolderMemento CreateMemento()
        {
            return new UrlHolderMemento(_urlsWithDates);
        }
    }
}