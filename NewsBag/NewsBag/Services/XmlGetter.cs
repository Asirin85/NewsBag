using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NewsBag.Services
{
    public class XmlGetter :IDisposable
    {
        private static WebClient _client;
        private bool _disposed;
        public XmlGetter()
        {
            _client = new WebClient();
        }
        public Stream GetNews(string sourceLink)
        {
            if (_disposed) { throw new ObjectDisposedException(_client.GetType().FullName); }
            _client.Headers.Add("user-agent", "MyRSSReader/1.0");
            var result = _client.OpenRead(sourceLink);
            return result;
        }
        public void Dispose()
        {
            if (_disposed) { return; }
            _client.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}
