using System;
using System.Threading;

namespace URLUpdateTestWithMoq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting a monitor");
            var monitor = new UrlMonitor();
            
            var observer1 = new UrlObserver("https://google.com");
            var observer2 = new UrlObserver("https://pja.edu.pl");

            monitor.AddObserver(observer1);
            monitor.AddObserver(observer1);
            monitor.AddObserver(observer2);
            monitor.CheckUrls();
            monitor.CheckUrls();
        }
    }
}