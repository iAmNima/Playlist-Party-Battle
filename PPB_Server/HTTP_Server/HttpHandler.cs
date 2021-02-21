using System;

using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Threading;
using System.Web;
using System.Collections.Generic;

namespace PPB.HTTP_Server
{
    public class HttpHandler
    {


        public TcpClient socket;
        

        private Stream inputStream;
        public StreamWriter outputStream;

        public String httpMethod;
        public String httpUrl;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();


        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpHandler(TcpClient client)
        {
            this.socket = client;
           
        }


        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }
        public void process()
        {

            inputStream = new BufferedStream(socket.GetStream());

            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try
            {
                parseRequest();
                readHeaders();
                if (httpMethod.Equals("GET"))
                {
                   // HandleRequest(this, null);
                    HandleRequest( null);
                }
                else if (httpMethod.Equals("POST"))
                {
                    StreamReader sr = GetStreamRaderFromPostData();

                    //HandleRequest(this, sr);

                    HandleRequest(sr);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());

                SendFailureResponseToClient(ResponseRepository.NotFound);
            }
            outputStream.Flush();
            // bs.Flush(); // flush any remaining output
            inputStream = null; outputStream = null; // bs = null;            
            socket.Close();
        }

        public void parseRequest()
        {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            httpMethod = tokens[0].ToUpper();
            httpUrl = tokens[1];
            http_protocol_versionstring = tokens[2];

            Console.WriteLine("starting: " + request);
        }
        public void readHeaders()
        {
            Console.WriteLine("readHeaders()");
            String line;
            while ((line = streamReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    Console.WriteLine("got headers");
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                Console.WriteLine("header: {0}:{1}", name, value);
                httpHeaders[name] = value;
            }
        }

        private const int BUF_SIZE = 4096;
        public StreamReader GetStreamRaderFromPostData()
        {

            Console.WriteLine("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length"))
            {
                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                if (content_len > MAX_POST_SIZE)
                {
                    throw new Exception(
                        String.Format("POST Content-Length({0}) too big for this simple server",
                          content_len));
                }
                byte[] buf = new byte[BUF_SIZE];
                int to_read = content_len;
                while (to_read > 0)
                {
                    Console.WriteLine("starting Read, to_read={0}", to_read);

                    int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                    Console.WriteLine("read finished, numread={0}", numread);
                    if (numread == 0)
                    {
                        if (to_read == 0)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("client disconnected during post");
                        }
                    }
                    to_read -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            return new StreamReader(ms);

        }


        public void SendSuccesResponseToClient(string retValue)
        {

            this.outputStream.WriteLine($"HTTP/1.1 200 OK");

            this.outputStream.WriteLine("Content-Type: " + "text/html");

            this.outputStream.WriteLine("Connection: close");

            this.outputStream.WriteLine("");

            this.outputStream.Write(retValue);

            this.outputStream.Write("\r\n\r\n");
        }

        public void SendFailureResponseToClient( string strResponse)
        {

            string[] resp = strResponse.Split(',');
            //  p.outputStream.WriteLine($"HTTP/1.1 {resp[0]} OK \r\n");
            this.outputStream.WriteLine($"HTTP/1.1 {resp[0]} OK");

            this.outputStream.WriteLine("Content-Type: " + "text/html");
            this.outputStream.WriteLine("Connection: close");
            this.outputStream.WriteLine("");

            this.outputStream.Write(resp[1]);

            this.outputStream.Write("\r\n\r\n");

        }


       // public void HandleRequest(HttpHandler httpHandler, StreamReader inputData)
        public void HandleRequest(StreamReader inputData)
        {

            string retValue = string.Empty;

            string data = string.Empty;

            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0}  request: {1}", this.httpMethod, this.httpUrl);

            Console.ForegroundColor = ConsoleColor.White;

            try
            {

                if (this.httpHeaders["Authorization"].ToString() == new Settings().Token)

                {

                    string strUrl = HttpUtility.UrlDecode(this.httpUrl);

                    string[] param = this.httpUrl.Split('/');

                    Dictionary<string, string> urlParams = new Dictionary<string, string>();

                    urlParams.Add("endPoint", param[1]);

                    urlParams.Add("actionMethod", param[2]);

                    if (this.httpMethod.ToUpper() == "POST") // Post Request
                    {
                        data = inputData.ReadToEnd();

                        if (data.Trim() != string.Empty)
                        {
                            urlParams.Add("data", data);
                        }

                    }
                    else
                        urlParams.Add("data", HttpUtility.UrlDecode(param[3]));


                    switch (urlParams["endPoint"])

                    {
                        case "User":
                            retValue = new EndPoint_Handlers.User(urlParams).RetValue;
                            break;

                        case "Song":
                            retValue = new EndPoint_Handlers.Song(urlParams).RetValue;
                            break;

                        case "TPlayer":
                            retValue = new EndPoint_Handlers.TPlayer(urlParams).RetValue;
                            break;
                    }

                    SendSuccesResponseToClient(retValue);



                }
                else
                {

                    SendFailureResponseToClient(ResponseRepository.Unauthorized);

                }

            }
            catch (Exception)
            {

                SendFailureResponseToClient( ResponseRepository.BadRequest);


            }


        }
    }
}
