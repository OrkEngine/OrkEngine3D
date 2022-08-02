using System.IO;
using System.Net;
using System.Net.Sockets;

namespace OrkEngine3D.Networking;

public class Connection
{
    public ICommunicatable local;

    public NetworkStream stream;
    public ConnectionTarget target;

    public Connection(ICommunicatable local, ConnectionTarget target, NetworkStream stream)
    {
        this.stream = stream;
        this.local = local;
    }

    public void Send(byte[] data){
        stream.Write(data);    
        stream.Flush();   
    }

    public void Update(){
        local.Update(this);
    }

    public void Send(string message){
        byte[] data = System.Text.Encoding.Default.GetBytes(message);
        Send(data);       
    }
}