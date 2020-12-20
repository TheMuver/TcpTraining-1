using System.Runtime.ConstrainedExecution;
using System;
using System.Net;
using System.Net.Sockets;
using ClientClassNamespace;
using System.Collections.Generic;
using System.Threading;

namespace ListenerNamespace
{
    public class Listener
    {
        private TcpListener _server;
        private Dictionary<string, ClientClass> _connectedUsers = new Dictionary<string, ClientClass>();
        private bool isListening = false;


        public void Start()
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, port);
            _server.Start();
            StartWaitingForConnections();
        }

        private void StartWaitingForConnections()
        {   
            isListening = true;
            new Thread(() => {
            while (isListening)
            {
                TcpClient client = _server.AcceptTcpClient();
                AddNewConnection(client);
            }
            }).Start();
        }

        private void AddNewConnection(TcpClient connection)
        {
            ClientClass user = new ClientClass(connection);
            user.Connect();
            user.OnMessageReceived += (message) => {
                // Auth
                if(message.Contains("Auth:"))
                {
                    string username = message.Split(':')[1];
                    _connectedUsers.Add("Muver", user);
                    _connectedUsers.Remove(connection.Client.RemoteEndPoint.ToString());
                }

                if(message.Contains("MessageTo:"))
                {
                    string toUser = message.Split(':')[1];
                    string userMessage = message.Split(':')[2];
                    _connectedUsers[toUser].SendMessage(userMessage);
                }
            };
            _connectedUsers.Add(connection.Client.RemoteEndPoint.ToString(), user);
        }

        private void StopWaitingForConnections()
        {
            isListening = false;
        }

        public void Stop()
        {
            StopWaitingForConnections();
            foreach (var user in _connectedUsers.Values)
            {
                user.Disconnect();
            }
            _server.Stop();
        }
    }
}