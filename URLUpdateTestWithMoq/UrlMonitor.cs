using System;
using System.Net;

namespace URLUpdateTestWithMoq
{
    public class UrlMonitor : ISubject
    {
        private event EventHandler<string> UrlUpdated;
        private readonly UrlHolder _holder = new UrlHolder();
        
        public void CheckUrls()
        {
            var urls = _holder.GetList();
            urls.ForEach(p =>
            {
                var lastModified = getUpdatedDateTimeFromUrl(p.Key);
                if (lastModified != null && lastModified != p.Value)
                {
                    _holder.Put(p.Key, (DateTime) lastModified);
                    EventHandler<string> handler = UrlUpdated;
                    handler?.Invoke(this, p.Key);
                }
            });
        }

        private DateTime? getUpdatedDateTimeFromUrl(string url)
        {
            Uri myUri = new Uri(url);
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(myUri); 
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                var lastModified = myHttpWebResponse.LastModified;
                myHttpWebResponse.Close();
                return lastModified;
            }

            return null;
        }

        public void AddObserver(IObserver observer)
        {
            if (_holder.Contains(observer)) return;
            UrlUpdated += observer.HandleEvent;
            _holder.Put(observer.UrlAddress, new DateTime());
        }

        public void RemoveObserver(IObserver observer)
        {
            UrlUpdated -= observer.HandleEvent;
            _holder.Remove(observer.UrlAddress);
        }
    }
}