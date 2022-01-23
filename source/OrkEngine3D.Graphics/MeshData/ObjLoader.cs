﻿using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics.MeshData
{
    public static class ObjLoader
    {
        public static readonly Logger logger = Logger.Get("MeshLoader", "Graphics");
        public static ObjComplete LoadObjFromFile(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string text = File.ReadAllText(path);
            logger.Log(LogMessageType.DEBUG, "Loading OBJ");
            string material = "__null";

            string[] lines = text.Split('\n');
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            List<string> vmaterials = new List<string>();


            List<uint> tvertices = new List<uint>();
            List<uint> tuvs = new List<uint>();
            List<uint> tnormals = new List<uint>();

            string mtl = "";


            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line.StartsWith("v "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid OBJ file!");
                    float x = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(c[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(c[3], CultureInfo.InvariantCulture);
                    vertices.Add(new Vector3(x, y, z));
                }
                if (line.StartsWith("vt "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 3)
                        logger.Log(LogMessageType.FATAL, "Invalid OBJ file!");
                    float x = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(c[2], CultureInfo.InvariantCulture);
                    uvs.Add(new Vector2(x, y));
                }
                if (line.StartsWith("vn "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid OBJ file!");
                    float x = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(c[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(c[3], CultureInfo.InvariantCulture);
                    normals.Add(new Vector3(x, y, z));
                }
                if (line.StartsWith("f "))
                {
                    var c = line.Split(' ');
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid OBJ file!");

                    for (int s = 1; s < 4; s++)
                    {
                        string set = c[s];
                        string[] t = set.Split('/');

                        if (t.Length < 3)
                            logger.Log(LogMessageType.FATAL, "Invalid OBJ file!");

                        uint v = uint.Parse(t[0]);
                        uint u = uint.Parse(t[1]);
                        uint n = uint.Parse(t[2]);

                        tvertices.Add(v - 1);
                        tuvs.Add(u - 1);
                        tnormals.Add(n - 1);
                        vmaterials.Add(material);

                    }
                }
                if (line.StartsWith("mtllib "))
                {
                    mtl = line.Substring("mtllib ".Length).Trim();
                }
                if (line.StartsWith("usemtl "))
                {
                    material = line.Substring("mtllib ".Length).Trim();
                }
            }



            Dictionary<string, Material> materials = LoadMTLFromFile(dir + "/" + mtl);

            List<Vector3> fvertices = new List<Vector3>();
            List<Vector2> fuvs = new List<Vector2>();
            List<Vector3> fnormals = new List<Vector3>();
            List<uint> ftriangles = new List<uint>();
            List<int> fmaterials = new List<int>();

            for (int i = 0; i < tvertices.Count; i++)
            {
                uint vertexIndex = tvertices[i];
                uint normalIndex = tnormals[i];
                uint uvIndex = tuvs[i];
                fvertices.Add(vertices[(int)vertexIndex]);
                fnormals.Add(normals[(int)normalIndex]);
                fuvs.Add(uvs[(int)uvIndex]);
                ftriangles.Add((uint)i);
                fmaterials.Add(materials.Keys.ToList().IndexOf(vmaterials[i]));
            }

            tvertices.Clear();
            tuvs.Clear();
            tnormals.Clear();


            return new ObjComplete(new MeshInformation(fvertices.ToArray(), new Color4[0], fuvs.ToArray(), fnormals.ToArray(), ftriangles.ToArray(), fmaterials.ToArray()), materials.Values.ToArray());
        }

        private static Dictionary<string, Material> LoadMTLFromFile(string filepath)
        {
            logger.Log(LogMessageType.WARNING, "MTL Loader uses OrkGraphics Flavoured MTL! It doesnt work with all MTL files");
            string content = File.ReadAllText(filepath);
            Material material = new Material();
            string materialname = "__null";
            string[] lines = content.Split('\n', StringSplitOptions.TrimEntries);
            List<Texture> textures = new List<Texture>();

            Dictionary<string, Material> materials = new Dictionary<string, Material>();


            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.StartsWith("Ka "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid MTL file!");
                    float r = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float g = float.Parse(c[2], CultureInfo.InvariantCulture);
                    float b = float.Parse(c[3], CultureInfo.InvariantCulture);
                    material.ambient = new Color3(r, g, b);
                }
                if (line.StartsWith("Kd "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid MTL file!");
                    float r = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float g = float.Parse(c[2], CultureInfo.InvariantCulture);
                    float b = float.Parse(c[3], CultureInfo.InvariantCulture);
                    material.diffuse = new Color3(r, g, b);
                }
                if (line.StartsWith("Ks "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 4)
                        logger.Log(LogMessageType.FATAL, "Invalid MTL file!");
                    float r = float.Parse(c[1], CultureInfo.InvariantCulture);
                    float g = float.Parse(c[2], CultureInfo.InvariantCulture);
                    float b = float.Parse(c[3], CultureInfo.InvariantCulture);
                    material.specular = new Color3(r, g, b);
                }
                if (line.StartsWith("Ns "))
                {
                    var c = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (c.Length < 2)
                        logger.Log(LogMessageType.FATAL, "Invalid MTL file!");
                    material.shininess = float.Parse(c[1], CultureInfo.InvariantCulture);
                }
                if (line.StartsWith("Tx "))
                {
                    string path = line.Substring("Tx ".Length).Trim();
                    textures.Add(new Texture(Rendering.currentContext.glmanager, Texture.GetTextureDataFromFile(path)));
                }
                if(line.StartsWith("newmtl "))
                {
                    material.textures = textures.ToArray();
                    textures.Clear();
                    materials.Add(materialname, material);
                    material = new Material();
                    materialname = line.Substring("newmtl ".Length).Trim();
                }

            }

            material.textures = textures.ToArray();
            materials.Add(materialname, material);


            return materials;
        }
    }

    public struct ObjComplete
    {
        public MeshInformation meshInformation;
        public Material[] materials;

        public ObjComplete(MeshInformation meshInformation, Material[] materials)
        {
            this.meshInformation = meshInformation;
            this.materials = materials;
        }
    }
}
