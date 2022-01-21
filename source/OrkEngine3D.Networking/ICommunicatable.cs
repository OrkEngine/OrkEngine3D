using System;

namespace OrkEngine3D.Networking
{
    public interface ICommunicatable
    {
        public static int BUFFER_SIZE = 1024;
        public abstract void Recieve(byte[] data);

        void Update(Connection connection){
            if(connection.stream.CanRead){
                byte[] buffer = new byte[1024];
                int bytesRead = connection.stream.Read(buffer);
                if(bytesRead > 0){
                    Recieve(buffer);
                }

            }
            
        }
    }
}