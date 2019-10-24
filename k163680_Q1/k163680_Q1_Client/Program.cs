using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace k163680_Q1_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        static void ExecuteClient()
        {
            try
            {

                IPEndPoint socket = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings.Get("portNo")));

                // Creation TCP/IP Socket using  
                // Socket Class Costructor 
                Socket sender = new Socket(IPAddress.Loopback.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(socket);
                    // We print EndPoint information  
                    // that we are connected 
                    Console.WriteLine("Socket connected to -> {0} ",sender.RemoteEndPoint.ToString());
                    while (true)
                    {
                        Console.WriteLine("Enter any string to reverse..");
                        Console.WriteLine("Enter 'Exit' to close client..");
                        string message = Console.ReadLine();
                        if (message.ToLower() == "exit")
                            break;

                        byte[] messageSend = Encoding.ASCII.GetBytes(message);
                        int byteSent = sender.Send(messageSend);

                        // Data buffer 
                        byte[] messageReceived = new byte[1024];

                        int byteRecieved = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server [Reversed String] -> {0}", Encoding.ASCII.GetString(messageReceived, 0, byteRecieved));
                        messageReceived = null;
                        messageSend = null;
                        byteRecieved = 0;
                    }
                    sender.Send(Encoding.ASCII.GetBytes("Exit"));
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    Console.ReadLine();

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
    }
}
