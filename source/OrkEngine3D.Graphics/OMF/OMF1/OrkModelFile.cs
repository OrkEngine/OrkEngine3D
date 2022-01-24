using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The OMF1 namespace. All types have been renamed to C# naming conventions.
/// Currently following <see cref="https://github.com/OrkEngine/OMF-Specification/blob/main/OMF%201/1.0.0.md">OMF1 1.0.0 specification</see>.
/// </summary>
namespace OrkEngine3D.Graphics.OMF.OMF1
{
    /// <summary>
    /// The model itself.
    /// </summary>
    public struct OrkModelFile
    {
        public string version = "OMF1 1.0.0";
        public string copyright = "Copyright not specified";
        public string author = "Author not specified";
        public Node[] nodes = new Node[0];
        public Mesh[] meshes = new Mesh[0];
        public Material[] materials = new Material[0];

        public static OrkModelFile LoadFromJSONData(string data)
        {
            return JsonConvert.DeserializeObject<OrkModelFile>(data);
        }

        public static OrkModelFile LoadFromFile(string path)
        {
            return LoadFromJSONData(File.ReadAllText(path));
        }

        public static string SerializeModel(OrkModelFile file)
        {
            return JsonConvert.SerializeObject(file);
        }

        public static void SaveToFile(string path, OrkModelFile file)
        {
            File.WriteAllText(path, SerializeModel(file));
        }
    }

    /// <summary>
    /// A node, or bone of the model
    /// </summary>
    public struct Node
    {
        public string name = "NULL";
        public Transformation transformation = new Transformation();
        public uint[] mesh = new uint[0];
    }

    /// <summary>
    /// The mesh of a model
    /// </summary>
    public struct Mesh
    {
        public Vec3[] verticies = new Vec3[0];
        public Vec3[] normals = new Vec3[0];
        public Vec3[] colors = new Vec3[0];
        public Vec2[] uvs = new Vec2[0];
        public uint[] triangles = new uint[0];
        public uint[] materials = new uint[0];
    }

    /// <summary>
    /// The material of a model
    /// </summary>
    public struct Material
    {
        public string name = "NULL";
        public Vec3 ambient = new Vec3();
        public Vec3 diffuse = new Vec3();
        public Vec3 specular = new Vec3();
        public float shininess = 0.0f;
        public string[] textures = new string[0];
    }

    /// <summary>
    /// Model transformation
    /// </summary>
    public struct Transformation
    {
        public Vec3 position = new Vec3();
        public Vec3 rotation = new Vec3();
        public Vec3 scale = new Vec3();
        public Vec3 origin = new Vec3();
    }

    /// <summary>
    /// A 2D vector
    /// </summary>
    public struct Vec2
    {
        public float x, y, z = 0;

        public Vec2(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    /// <summary>
    /// A 3D vector
    /// </summary>
    public struct Vec3
    {
        public float x, y, z = 0;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
