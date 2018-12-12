using System;

namespace URLUpdateTestWithMoq
{
    public class UrlObserver : IObserver
    {
        public UrlObserver(string urlAddress)
        {
            UrlAddress = urlAddress;
        }

        public string UrlAddress { get; set; }

        public void HandleEvent(object sender, string url)
        {
            if (url == UrlAddress)
                Console.WriteLine($"{url} has been updated!");
        }
    }
}