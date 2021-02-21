using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PPB.HTTP_Server;

namespace PPB
{
    class Program
    {
        public static int Main(String[] args)
        {


            HTTPServer httpServer;

            if (args.GetLength(0) > 0)
            {
                httpServer = new HTTPServer(Convert.ToInt16(args[0]));
            }
            else
            {
                httpServer = new HTTPServer(new Settings().PortToListen);
            }
            Thread thread = new Thread(new ThreadStart(httpServer.ListenToClients));
            thread.Start();
            return 0;
        }

    }
}
