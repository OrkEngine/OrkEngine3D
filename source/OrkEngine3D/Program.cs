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
            Console.WriteLine("Network demo 1.0");
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
            string m = System.Text.Encoding.Default.GetString(data).Trim('\0');
            if(m == "SERVER_STOP")
                baseClient.Close();
            Console.WriteLine(m);
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
            string message = System.Text.Encoding.Default.GetString(data).Trim().Trim('\0');
            if(message.StartsWith("/")){
                string[] cmd = message.Substring(1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                switch(cmd[0].Trim()){
                    case ("say"):
                        Send($"[SERVER] {message.Substring("/say ".Length)}");
                        break;
                    case ("caps"):
                        Send(message.Substring("/say ".Length).ToUpper());
                        break;
                    case ("stop"):
                        Send("SERVER_STOP");
                        baseServer.Close();
                        break;
                    default:
                        Send("Invalid Command: " + message);
                        break;
                }
            } else{
                Send("[Client] " + message);
            }
        }
    }
}
