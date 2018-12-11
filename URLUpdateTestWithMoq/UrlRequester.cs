using System;
using System.Net;

namespace URLUpdateTestWithMoq
{
    public class UrlRequester : IUrlRequester
    {
        public DateTime? GetUpdatedDateTimeFromUrl(string url)
        {
            Uri myUri = new Uri(url);
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(myUri); 
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
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