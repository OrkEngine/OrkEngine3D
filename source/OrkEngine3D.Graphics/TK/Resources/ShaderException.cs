namespace OrkEngine3D.Graphics.TK.Resources
{
    public class ShaderException : System.Exception
    {
        public ShaderException() {}
        public ShaderException(string message) : base(message) {}
        public ShaderException(string message, System.Exception inner) : base(message, inner) {}
        public ShaderException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
    }

    public class ProgramException : System.Exception
    {
        public ProgramException() {}
        public ProgramException(string message) : base(message) {}
        public ProgramException(string message, System.Exception inner) : base(message, inner) {}
        public ProgramException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
    }
}