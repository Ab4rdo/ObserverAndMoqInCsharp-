using System;

namespace URLUpdateTestWithMoq
{
    public interface ISubject
    {
        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);
    }
}