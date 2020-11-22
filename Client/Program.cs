using System;
using System.Net.Sockets;

using ClientClassNamespace;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect("127.0.0.1", "String message");
            ClientClass client = new ClientClass("127.0.0.1", 13000);
            client.OnMessageReceived += (message) => { Console.WriteLine("Message received: " + message); };
            client.Connect();
            client.SendMessage("Hello world");
            client.Disconnect();
        }

        // Functions:
        // Connect()
        // SendMessage()
        // StartListening()
        // + OnMessageReceived
        // StopListening()
        // Disconnect()

        // static void Connect(String server, String message)
        // {
        //     try
        //     {
        //         #region Connect

        //         #endregion Connect

        //         #region SendMessage
        //         Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        //         Console.WriteLine("Sent: {0}", message);
        //         #endregion SendMessage
                

        //         // String to store the response ASCII representation.
        //         String responseData = String.Empty;

        //         // Read the first batch of the TcpServer response bytes.
                
        //         responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        //         Console.WriteLine("Received: {0}", responseData);

        //         // Close everything.
        //         stream.Close();
        //         client.Close();
        //     }
        //     catch (ArgumentNullException e)
        //     {
        //         Console.WriteLine("ArgumentNullException: {0}", e);
        //     }
        //     catch (SocketException e)
        //     {
        //         Console.WriteLine("SocketException: {0}", e);
        //     }

        //     Console.WriteLine("\n Press Enter to continue...");
        //     Console.Read();
        // }
    }
}
