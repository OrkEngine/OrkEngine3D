/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using OrkEngine3D.Diagnostics.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OrkEngine3D.Networking;

public class Server : ICommunicatable
{
    
    private TcpListener listener;
    public Connection connection;
    private ServerInterface serverInterface;
    private ConnectionTarget target;
    Logger logger;
    public bool multiThreaded;

    private Dictionary<int, TcpClient> connectedClients = new Dictionary<int, TcpClient>();
    private Dictionary<int, bool> activeThreads = new Dictionary<int, bool>();

    public Server(ServerInterface serverInterface, ConnectionTarget target, bool multiThreaded = false){
        this.serverInterface = serverInterface;
        this.serverInterface.baseServer = this;
        this.multiThreaded = multiThreaded;

        Open(target);
    }

    private void Open(ConnectionTarget target){ 
        logger = Logger.Get(target.ToString(), "NetworkingServer");

        logger.Log(LogMessageType.INFORMATION, "Opening port");

        TcpListener listener = new TcpListener(IPAddress.Parse(target.ip), target.port);
        this.listener = listener;
        this.target = target;
        if(multiThreaded){
            Thread serverThread = new Thread(() => { Listen(); });
            serverThread.IsBackground = true;
            serverThread.Start();
            Thread.Sleep(1000);
        } else{
            Listen();
        }
        
    }

    public void Listen(){
        logger.Log(LogMessageType.INFORMATION, "Listening at " + target);
        listener.Start(10);
        TcpClient client = listener.AcceptTcpClient();

        Thread clientThread = new Thread(ConnectToClient);
        clientThread.IsBackground = true;
        clientThread.Start(client);

        Listen();
    }

    public void ConnectToClient(object cliento){
        TcpClient client = (TcpClient)cliento;
        connection = new Connection(this, target, client.GetStream());
        connectedClients.Add(Thread.CurrentThread.ManagedThreadId, client);
        logger.Log(LogMessageType.INFORMATION, "Connected at " + target);

        serverInterface.OnConnect((client.Client.RemoteEndPoint as IPEndPoint).Address.ToString());
        SetThreadActive(true);
        while(IsThreadActive() && client.Connected){
            connection.Update();
            serverInterface.MainLoop();
        }
    }
    

    private bool IsThreadActive(){
        return activeThreads[Thread.CurrentThread.ManagedThreadId];
    }

    private void SetThreadActive(bool value){
        if(!activeThreads.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            activeThreads.Add(Thread.CurrentThread.ManagedThreadId, value);
        else
            activeThreads[Thread.CurrentThread.ManagedThreadId] = value;
    }

    public void Close(){
        connectedClients[Thread.CurrentThread.ManagedThreadId].GetStream().Close();
        connectedClients[Thread.CurrentThread.ManagedThreadId].Close();

        connectedClients.Remove(Thread.CurrentThread.ManagedThreadId);
        SetThreadActive(false);
    }

    public void Recieve(byte[] data)
    {
        serverInterface.OnRecieve(data);
    }
}