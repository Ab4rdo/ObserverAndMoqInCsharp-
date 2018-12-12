using System;

namespace URLUpdateTestWithMoq
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting a monitor");
            var monitor = new UrlMonitor();

            var observer1 = new UrlObserver("https://google.com");
            var observer2 = new UrlObserver("https://pja.edu.pl");

            monitor.AddObserver(observer1);
            monitor.CheckUrls();
            monitor.CheckUrls();
            monitor.CheckUrls();
        }
    }
}