using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace OrkEngine3D.Diagnostics
{
    public class Debugger
    {
        /* TODO: could break in future versions let see */
        [Conditional("DEBUG")]
        public static void CheckGLError(string title)
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                Debug.Print($"{title}: {error}");
            }
        }
    }

    public class Debug
    {
        public static void Print(string msg)
        {
            //pass internal debugger and custom debugger

            //intenrel
            System.Diagnostics.Debug.Print(msg);
            //custom
            Console.WriteLine(msg);
        }
    }

    /* TODO: Move later, can remain for testing */

    //[Orkbranding] -- [QualifiedName("OrkEngine3D.GLOperation")]
    //testing move to graphics
    [QualifiedName("OrkEngine3D.Diagnostics.GLOperation")]
    public class GLOperation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LabelObject(ObjectLabelIdentifier objectLabelIdentifier, int glObject, string name)
        {
            GL.ObjectLabel(objectLabelIdentifier, glObject, name.Length, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTexture(TextureTarget target, string name, out int texture)
        {
            GL.CreateTextures(target, 1, out texture);
            LabelObject(ObjectLabelIdentifier.Texture, texture, $"[texture]: `{name}`"); // [texture(`test4`)]: `example` //<- doesn't work
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateProgram(string name, out int program)
        {
            program = GL.CreateProgram();
            LabelObject(ObjectLabelIdentifier.Program, program, $"[program]: `{name}`");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateShader(ShaderType type, string name, out int shader)
        {
            shader = GL.CreateShader(type);
            LabelObject(ObjectLabelIdentifier.Shader, shader, $"[shader]: `{type}`~>`{name}`"); //[shader]: `fragment`~>`example`
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateBuffer(string name, out int buffer)
        {
            GL.CreateBuffers(1, out buffer);
            LabelObject(ObjectLabelIdentifier.Buffer, buffer, $"[buffer]: `{name}`");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateVertexBuffer(string name, out int buffer)
            => CreateBuffer($"[VBO]: `{name}`", out buffer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateElementBuffer(string name, out int buffer)
            => CreateBuffer($"[EBO]: `{name}`", out buffer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateVertexArray(string name, out int vao)
        {
            GL.CreateVertexArrays(1, out vao);
            LabelObject(ObjectLabelIdentifier.VertexArray, vao, $"[VAO]: `{name}`");
        }
    }

   
}
