using System.Text;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;

namespace ClientClassNamespace
{
    public class ClientClass
    {
        private readonly string _serverAddress;
        private readonly int _port;
        private NetworkStream _stream;
        private Thread _listeningThread;

        public event Action<string> OnMessageReceived;

        public ClientClass(string serverAddress, int port)
        {
            _serverAddress = serverAddress;
            _port = port;
        }

        public void Connect()
        {
            TcpClient client = new TcpClient(_serverAddress, _port);
            _stream = client.GetStream();
            StartListening();
        }

        public void SendMessage(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }

        private void StartListening()
        {
            _listeningThread = new Thread(() =>
            {
                // todo fix infinity loop
                while (true)
                {
                    byte[] data = new byte[256];
                    Int32 bytes = _stream.Read(data, 0, data.Length);
                    string message = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    OnMessageReceived?.Invoke(message);
                }
            });
            _listeningThread.Start();
        }
    }
}