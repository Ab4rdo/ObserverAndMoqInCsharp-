using System;

namespace URLUpdateTestWithMoq
{
    public interface IObserver
    {
        string UrlAddress { get; }
        
        void HandleEvent(object sender, string args);
    }
}