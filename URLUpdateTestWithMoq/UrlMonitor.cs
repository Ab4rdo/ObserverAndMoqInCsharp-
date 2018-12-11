using System;
using System.Net;

namespace URLUpdateTestWithMoq
{
    public class UrlMonitor : ISubject
    {
        private event EventHandler<string> UrlUpdated;
        private readonly UrlHolder _holder = new UrlHolder();
        private readonly IUrlRequester _requester = new UrlRequester();

        public UrlMonitor() {}
        public UrlMonitor(IUrlRequester requester) => _requester = requester;

        public void CheckUrls()
        {
            var urls = _holder.GetList();
            urls.ForEach(p =>
            {
                var lastModified = _requester.GetUpdatedDateTimeFromUrl(p.Key);
                if (lastModified != null && lastModified != p.Value)
                {
                    _holder.Put(p.Key, (DateTime) lastModified);
                    EventHandler<string> handler = UrlUpdated;
                    handler?.Invoke(this, p.Key);
                }
            });
        }

        public void AddObserver(IObserver observer)
        {
            UrlUpdated -= observer.HandleEvent;
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