using System;

namespace URLUpdateTestWithMoq
{
    public class UrlObserver : IObserver
    {
        public string UrlAddress { get; }

        public UrlObserver(string urlAddress)
        {
            UrlAddress = urlAddress;
        }

        public void HandleEvent(object sender, string url)
        {
            if(url == UrlAddress)
                Console.WriteLine($"{url} has been updated!");
        }
    }
}