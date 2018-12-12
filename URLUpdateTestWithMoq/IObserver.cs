namespace URLUpdateTestWithMoq
{
    public interface IObserver
    {
        string UrlAddress { get; set; }

        void HandleEvent(object sender, string args);
    }
}