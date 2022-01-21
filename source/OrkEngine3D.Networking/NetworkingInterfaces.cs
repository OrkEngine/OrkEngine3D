namespace OrkEngine3D.Networking
{
    public abstract class ClientInterface
    {
        public Client baseClient;
        public bool acceptReading = true;

        public abstract void OnRecieve(byte[] data);
        public virtual void OnConnect(){}

        public abstract void MainLoop();
        public void Send(byte[] data){
            baseClient.connection.Send(data);
        }
        public void Send(string data){
            baseClient.connection.Send(data);
        }
    }

    public abstract class ServerInterface
    {
        public Server baseServer;

        public abstract void OnRecieve(byte[] data);
        public virtual void OnConnect(string clientip){}

        public abstract void MainLoop();
        public void Send(byte[] data){
            baseServer.connection.Send(data);
        }
        public void Send(string data){
            baseServer.connection.Send(data);
        }
    }
}