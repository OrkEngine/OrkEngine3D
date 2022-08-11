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

using System;
using System.Net.Sockets;
using System.Threading;
using OrkEngine3D.Diagnostics.Logging;

namespace OrkEngine3D.Networking;

public class Client : ICommunicatable
{
    public Connection connection;
    private TcpClient client;
    private Logger logger;
    private ClientInterface clientInterface;
    private Thread mlThread;
    private bool keepThreadUp;

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
        keepThreadUp = true;
        MainLoop();       
        
    }

    public void Close(){
        client.GetStream().Close();
        client.Close();
        keepThreadUp = false;
    }

    private void MainLoop(){
        
        while(keepThreadUp && client.Connected){
            connection.Update();
            if(keepThreadUp)
                clientInterface.MainLoop();
        }
    }

    public void Recieve(byte[] data)
    {
        clientInterface.OnRecieve(data);
    }
}