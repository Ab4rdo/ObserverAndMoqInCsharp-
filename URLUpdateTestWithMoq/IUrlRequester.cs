using System;

namespace URLUpdateTestWithMoq
{
    public interface IUrlRequester
    {
        DateTime? GetUpdatedDateTimeFromUrl(string url);
    }
}