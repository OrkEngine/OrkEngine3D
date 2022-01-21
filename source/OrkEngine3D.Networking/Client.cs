using System;
using System.Net.Sockets;
using System.Threading;
using OrkEngine3D.Diagnostics.Logging;

namespace OrkEngine3D.Networking
{
    public class Client : ICommunicatable
    {
        public Connection connection;
        private TcpClient client;
        private Logger logger;
        private ClientInterface clientInterface;

        public Client(ClientInterface clientInterface, ConnectionTarget target)
        {
            this.clientInterface = clientInterface;
            this.clientInterface.baseClient = this;
            Connect(target);
        }

        private void Connect(ConnectionTarget target)
        {
            this.logger = new Logger("Client-" + Guid.NewGuid().ToString(), "NetworkClient");
            logger.Log(LogMessageType.INFORMATION, "Connecting to " + target);
            this.client = new TcpClient(target.ip, target.port);
            logger.Log(LogMessageType.INFORMATION, "Client connected to "+ target);
            this.connection = new Connection(this, target, client.GetStream());
            
            clientInterface.OnConnect();

            Thread mlThread = new Thread(MainLoop);        
            mlThread.Start();
            
        }

        private void MainLoop(){
            
            while(true){
                connection.Update();
                clientInterface.MainLoop();
            }
        }

        public void Recieve(byte[] data)
        {
            clientInterface.OnRecieve(data);
        }
    }
}