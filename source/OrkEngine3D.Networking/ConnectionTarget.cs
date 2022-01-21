namespace OrkEngine3D.Networking
{
    public struct ConnectionTarget
    {
        public string ip;
        public int port;

        public ConnectionTarget(string ip, int port = 25500)
        {
            this.ip = ip;
            this.port = port;
        }

        public override string ToString()
        {
            return ip + ":" + port;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ConnectionTarget)){
                ConnectionTarget c = (ConnectionTarget)obj;
                return c.ip == ip && c.port == port;
            }
            return false;
        }
    }
}