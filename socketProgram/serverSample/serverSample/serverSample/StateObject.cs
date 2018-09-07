using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace serverSample
{
    class StateObject
    {
        public Socket workSocket { get; set; }
        public const int BUFFER_SIZE = 1024;
        internal byte[] buffer = new byte[BUFFER_SIZE];
    }
}
