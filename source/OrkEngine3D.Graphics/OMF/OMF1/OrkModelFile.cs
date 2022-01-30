using Newtonsoft.Json;
using OrkEngine3D.Mathematics;
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

        public OrkModelFile()
        {
        }

        public static OrkModelFile LoadFromJSONData(string data)
        {
            return JsonConvert.DeserializeObject<OrkModelFile>(data);
        }

        public static MeshData.ObjComplete OMFToObjComplete(OrkModelFile file){
            MeshData.ObjComplete complete = new MeshData.ObjComplete();
            complete.materials = new TK.Material[file.materials.Length];

            for(int i = 0; i < complete.materials.Length; i++)
            {
                Material material = file.materials[i];
                complete.materials[i] = new TK.Material{ 
                    diffuse = material.diffuse.ToColor3(),
                    specular = material.specular.ToColor3(),
                    ambient = material.ambient.ToColor3(),
                    shininess = material.shininess,
                };
            }
            Mesh mesh = file.meshes[0];
            
            Vector3[] verticies = new Vector3[mesh.verticies.Length];
            for (int i = 0; i < mesh.verticies.Length; i++)
            {
                verticies[i] = mesh.verticies[i].ToOrkMaths();
            }

            Vector2[] uvs = new Vector2[mesh.uvs.Length];
            for (int i = 0; i < mesh.verticies.Length; i++)
            {
                uvs[i] = mesh.uvs[i].ToOrkMaths();
            }

            Vector3[] normals = new Vector3[mesh.normals.Length];
            for (int i = 0; i < mesh.normals.Length; i++)
            {
                normals[i] = mesh.normals[i].ToOrkMaths();
            }

            Color4[] colors = new Color4[mesh.colors.Length];
            for (int i = 0; i < mesh.colors.Length; i++)
            {
                colors[i] = mesh.colors[i].ToColor4();
            }

            int[] materials = mesh.materials.Cast<int>().ToArray();
            uint[] triangles = mesh.triangles;

            MeshData.MeshInformation meshInfo = new MeshData.MeshInformation(verticies, colors, uvs, normals, triangles, materials);

            complete.meshInformation = meshInfo;
            
            return complete;
        }

        public static OrkModelFile ObjCompleteToOMF(MeshData.ObjComplete obj){
            OrkModelFile modelFile = new OrkModelFile();
            modelFile.author = "OrkEngine OMF Converter";
            modelFile.copyright = "Copyright OrkEngine 2021";
            modelFile.version = "1.1.0.0";

            modelFile.materials = new Material[obj.materials.Length];
            for(int i = 0; i < modelFile.materials.Length; i++)
            {
                TK.Material material = obj.materials[i];
                modelFile.materials[i] = new Material{ 
                    diffuse = material.diffuse.ToOMFVec3(),
                    specular = material.specular.ToOMFVec3(),
                    ambient = material.ambient.ToOMFVec3(),
                    shininess = material.shininess,
                };
            }

            modelFile.meshes = new Mesh[1];
            Mesh mesh = new Mesh();
            MeshData.MeshInformation meshInfo = obj.meshInformation;
            mesh.verticies = new Vec3[meshInfo.verticies.Length];
            for (int i = 0; i < mesh.verticies.Length; i++)
            {
                mesh.verticies[i] = meshInfo.verticies[i].ToOMFVec3();
            }

            mesh.uvs = new Vec2[meshInfo.uv.Length];
            for (int i = 0; i < mesh.verticies.Length; i++)
            {
                mesh.uvs[i] = meshInfo.uv[i].ToOMFVec2();
            }

            mesh.normals = new Vec3[meshInfo.normals.Length];
            for (int i = 0; i < mesh.normals.Length; i++)
            {
                mesh.normals[i] = meshInfo.normals[i].ToOMFVec3();
            }

            mesh.colors = new Vec3[meshInfo.colors.Length];
            for (int i = 0; i < mesh.colors.Length; i++)
            {
                mesh.colors[i] = meshInfo.colors[i].ToOMFVec3();
            }

            mesh.materials = meshInfo.materials.Cast<uint>().ToArray();
            mesh.triangles = meshInfo.triangles;

            modelFile.meshes[0] = mesh;
            modelFile.nodes = new Node[]{
                new Node{
                    name = "root",
                    mesh = new uint[] { 0 }
                }
            };

            return modelFile;

        }

        public static OrkModelFile LoadFromFile(string path)
        {
            return LoadFromJSONData(File.ReadAllText(path));
        }

        public static string SerializeModel(OrkModelFile file, bool compact = false)
        {
            return JsonConvert.SerializeObject(file, (compact ? Formatting.None : Formatting.Indented));
        }

        public static void SaveToFile(string path, OrkModelFile file, bool compact = false)
        {
            File.WriteAllText(path, SerializeModel(file, compact));
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

        public Node()
        {
        }
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

        public Mesh()
        {
        }
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

        public Material()
        {
        }
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

        public Transformation()
        {
        }
    }

    /// <summary>
    /// A 2D vector
    /// </summary>
    public struct Vec2
    {
        public float x, y = 0;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToOrkMaths(){
            return new Vector2(x, y);
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

        public Vector3 ToOrkMaths(){
            return new Vector3(x, y, z);
        }
        
        public Color3 ToColor3(){
            return new Color3(x, y, z);
        }

        public Color4 ToColor4(){
            return new Color4(1f, x, y, z);
        }
    }

    public static class Extentions
    {
        public static Vec3 ToOMFVec3(this Vector3 vec){
            return new Vec3(vec.X, vec.Y, vec.Z);
        }

        public static Vec3 ToOMFVec3(this Color4 vec){
            return new Vec3(vec.Red, vec.Green, vec.Blue);
        }

        public static Vec3 ToOMFVec3(this Color3 vec){
            return new Vec3(vec.Red, vec.Green, vec.Blue);
        }

        public static Vec2 ToOMFVec2(this Vector2 vec){
            return new Vec2(vec.X, vec.Y);
        }
    }
}