using System;
using System.Net;

namespace URLUpdateTestWithMoq
{
    public class UrlRequester : IUrlRequester
    {
        public DateTime? GetUpdatedDateTimeFromUrl(string url)
        {
            var myUri = new Uri(url);
            var myHttpWebRequest = (HttpWebRequest) WebRequest.Create(myUri);
            var myHttpWebResponse = (HttpWebResponse) myHttpWebRequest.GetResponse();
            if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                var lastModified = myHttpWebResponse.LastModified;
                myHttpWebResponse.Close();
                return lastModified;
            }

            return null;
        }
    }
}