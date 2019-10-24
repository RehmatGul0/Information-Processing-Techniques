using System;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
namespace k163680_Q1_Server
{
    class Program
    {
        public class HandleClinet
        {
            TcpClient clientSocket;
            string clientNumber;
            Thread clientThread;

            public void startClient(TcpClient clientSocket, string clientNumber)
            {
                this. clientSocket  = clientSocket;
                this.clientNumber = clientNumber;
                this.clientThread = new Thread(reverseString);
                this.clientThread.Start();
            }
            private void reverseString()
            {
                int requestCount = 0;
                byte[] bytesFrom = new byte[4096];
                Byte[] sendBytes = null;
                string serverResponse = null;

                while (true)
                {
                    try
                    {
                        StringBuilder clientData = new StringBuilder();
                        requestCount = requestCount + 1;
                        NetworkStream networkStream = clientSocket.GetStream();
                        int bytesRead = networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                        clientData.AppendFormat("{0}", Encoding.ASCII.GetString(bytesFrom,0, bytesRead));
                        Console.WriteLine("From client:"+" "+this.clientNumber +"  "+clientData);

                        if (clientData.ToString().ToLower() == "Exit") { this.clientThread.Abort(); networkStream.Flush(); }

                        char[] temp = clientData.ToString().ToCharArray();
                        Array.Reverse(temp);

                        serverResponse = new string(temp);
                        Console.WriteLine("To client:"+"  "+this.clientNumber+" "+serverResponse);

                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception:" + ex.ToString());
                        Console.ReadLine();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint socket = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings.Get("portNo")));
                TcpListener server = new TcpListener(socket);
                TcpClient clientSocket = default(TcpClient);
                int counter = 0;

                server.Start();
                Console.WriteLine("Server Started at " + socket.Address + ":" + socket.Port);

                counter = 0;
                Console.WriteLine("Press 'E' to exit..");
                while ((true))
                {
                    counter += 1;
                    clientSocket = server.AcceptTcpClient();
                    Console.WriteLine("Client No:" + counter + " started!");
                    HandleClinet client = new HandleClinet();
                    client.startClient(clientSocket, Convert.ToString(counter));
                }
            }
            catch
            {

            }
            
        }
    }   
}