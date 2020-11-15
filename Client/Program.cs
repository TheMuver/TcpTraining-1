using System;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect("127.0.0.1", "String message");
        }

        // Functions:
        // Connect()
        // SendMessage()
        // StartListening()
        // + OnMessageReceived
        // StopListening()
        // Disconnect()

        static void Connect(String server, String message)
        {
            try
            {
                #region Connect
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                #endregion Connect

                #region SendMessage
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);
                #endregion SendMessage
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
