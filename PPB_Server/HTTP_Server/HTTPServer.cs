using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace PPB.HTTP_Server
{
    public  class HTTPServer
    {

        protected int port;
        TcpListener listener;
        bool isActive = true;

        public HTTPServer(int port)
        {
            this.port = port;
        }
        public void ListenToClients()
        {
            listener = new TcpListener(IPAddress.Any, port);

            listener.Start();

            Console.ForegroundColor = ConsoleColor.Green ;

            Console.WriteLine(" HTTP Server Running...");

            Console.ForegroundColor = ConsoleColor.Yellow;

            while (isActive)
            {
                TcpClient client = listener.AcceptTcpClient();

                HttpHandler httpHandler = new HttpHandler(client);

                Thread thread = new Thread(new ThreadStart(httpHandler.process));

                thread.Start();

                Thread.Sleep(1);
            }
        }

    }
}
