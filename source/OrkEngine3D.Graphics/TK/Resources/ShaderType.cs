namespace OrkEngine3D.Graphics.Resources.TK
{
    public enum ShaderType
    {
        ComputeShader = OpenTK.Graphics.OpenGL4.ShaderType.ComputeShader,
        FragmentShader = OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader,
        VertexShader = OpenTK.Graphics.OpenGL4.ShaderType.VertexShader,
        FragmentShaderArb = OpenTK.Graphics.OpenGL4.ShaderType.FragmentShaderArb,
        VertexShaderArb = OpenTK.Graphics.OpenGL4.ShaderType.VertexShaderArb,
        GeometryShader = OpenTK.Graphics.OpenGL4.ShaderType.GeometryShader,
        TessControlShader = OpenTK.Graphics.OpenGL4.ShaderType.TessControlShader,
        TessEvaluationShader = OpenTK.Graphics.OpenGL4.ShaderType.TessEvaluationShader
    }
}