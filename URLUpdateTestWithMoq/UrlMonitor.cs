using System;

namespace URLUpdateTestWithMoq
{
    public class UrlMonitor : ISubject
    {
        private readonly UrlHolder _holder = new UrlHolder();
        private readonly IUrlRequester _requester = new UrlRequester();

        public UrlMonitor()
        {
        }

        public UrlMonitor(IUrlRequester requester)
        {
            _requester = requester;
        }

        /// <summary>
        ///     Observer is added for a UrlMonitor. Observer cannot be added twice (it's re-added).
        ///     After adding another observer for already observed url,
        ///     time of last modification will reset! IT IS EXPECTED BEHAVIOR FOR SURE.
        /// </summary>
        /// <param name="observer"></param>
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

        private event EventHandler<string> UrlUpdated;

        public void CheckUrls()
        {
            var urls = _holder.GetList();
            urls.ForEach(p =>
            {
                var lastModified = _requester.GetUpdatedDateTimeFromUrl(p.Key);
                if (lastModified != null && lastModified != p.Value)
                {
                    _holder.Put(p.Key, (DateTime) lastModified);
                    var handler = UrlUpdated;
                    handler?.Invoke(this, p.Key);
                }
            });
        }
    }
}