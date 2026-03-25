using System;
using System.Collections.Generic;
using System.Text;

namespace LF_8_Server
{
    internal class MonitoredClient(string hostname, string url)
    {
        public string Hostname = hostname;
        private string _url = url;
    }
}
