using System;
using OrkEngine3D.Networking;
using OrkEngine3D.Diagnostics.Logging;
using System.Threading;

namespace OrkEngine3D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server(new TestServer(), new ConnectionTarget("127.0.0.1"));
            Client client = new Client(new TestClient(), new ConnectionTarget("127.0.0.1"));
        }
    }

    public class TestClient : ClientInterface
    {

        public override void OnConnect(){
            Send("CONNECTED");
        }

        public override void MainLoop()
        {
            Console.Write("> ");
            Send(Console.ReadLine());
            Thread.Sleep(100); 
        }

        public override void OnRecieve(byte[] data)
        {
            Console.WriteLine(System.Text.Encoding.Default.GetString(data));
        }
    }

    public class TestServer : ServerInterface
    {
        public override void MainLoop()
        {
            
        }

        public override void OnConnect(string ip)
        {
            //Send($"[Server] {ip} CONNECTED");
        }

        public override void OnRecieve(byte[] data)
        {
            Send("[Server]" + System.Text.Encoding.Default.GetString(data));
        }
    }
}
