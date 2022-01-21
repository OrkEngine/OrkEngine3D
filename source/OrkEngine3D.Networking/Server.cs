using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using OrkEngine3D.Diagnostics.Logging;

namespace OrkEngine3D.Networking
{
    public class Server : ICommunicatable
    {
        
        private TcpListener listener;
        public Connection connection;
        private ServerInterface serverInterface;
        private ConnectionTarget target;
        Logger logger;

        public Server(ServerInterface serverInterface, ConnectionTarget target){
            this.serverInterface = serverInterface;
            this.serverInterface.baseServer = this;

            Open(target);
        }

        private void Open(ConnectionTarget target){ 
            logger = Logger.Get(target.ToString(), "NetworkingServer");

            logger.Log(LogMessageType.INFORMATION, "Opening port");

            TcpListener listener = new TcpListener(IPAddress.Parse(target.ip), target.port);
            this.listener = listener;
            this.target = target;
            Thread serverThread = new Thread(() => { Listen(); });
            serverThread.Start();
            Thread.Sleep(1000);
        }

        public void Listen(){
            logger.Log(LogMessageType.INFORMATION, "Listening at " + target);
            listener.Start(10);
            TcpClient client = listener.AcceptTcpClient();
            connection = new Connection(this, target, client.GetStream());
            logger.Log(LogMessageType.INFORMATION, "Connected at " + target);

            serverInterface.OnConnect((client.Client.RemoteEndPoint as IPEndPoint).Address.ToString());

            while(true){
                connection.Update();
                serverInterface.MainLoop();
            }
        }


        public void Recieve(byte[] data)
        {
            serverInterface.OnRecieve(data);
        }
    }
}