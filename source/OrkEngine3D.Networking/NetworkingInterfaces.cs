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

namespace OrkEngine3D.Networking;

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