using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class Texture : GLResource
    {
        public int id;
        public readonly int width;
        public readonly int height;

        /// <summary>
        /// Generate a texture from data
        /// </summary>
        /// <param name="manager">he OpenGL manager</param>
        /// <param name="data">The texture data</param>
        /// <returns>The texture object</returns>
        public Texture(GLResourceManager manager, TextureData data) : base(manager)
        {
            this.width = data.width;
            this.height = data.height;

            id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.width, data.height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data.pixels);
        }

        /// <summary>
        /// Generate a texture object from id
        /// WARNING: UNSAFE!
        /// </summary>
        /// <param name="id">The OpenGL texture ID</param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        public Texture(GLResourceManager manager, int id, int width, int height) : base(manager)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }
        
        /// <summary>
        /// Use a texture for rendering
        /// </summary>
        /// <param name="id">The texure ID(in shader)</param>
        public void Use(byte id){
            if(id > 31 && id < 0)
                throw new IndexOutOfRangeException("Texture index may only be 0-31");
            GL.ActiveTexture((TextureUnit)Enum.Parse(typeof(TextureUnit), "Texture"+id.ToString()));
            GL.BindTexture(TextureTarget.Texture2D, this.id);
        }

        /// <summary>
        /// Unload the texture
        /// </summary>

        public override void Unload()
        {
            GL.DeleteTexture(id);
        }

        /// <summary>
        /// Get the texture data from a file
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The files texturedata</returns>
        public static TextureData GetTextureDataFromFile(string path){
            return GetTextureDataFromFileData(File.ReadAllBytes(path));
        }


        /// <summary>
        /// Get the texture data from the contents of a .png etc. The raw file data.
        /// </summary>
        /// <param name="data">The raw file data</param>
        /// <returns>The texture data</returns>
        public static TextureData GetTextureDataFromFileData(byte[] data)
        {
            //Load the image
            Image<Rgba32> image = Image.Load<Rgba32>(data);

            //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            //This will correct that, making the texture display properly.
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = new List<byte>(4 * image.Width * image.Height);

            for (int y = 0; y < image.Height; y++) {
                var row = image.GetPixelRowSpan(y);

                for (int x = 0; x < image.Width; x++) {
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            return new TextureData(image.Width, image.Height, pixels.ToArray());
        }
    }

    /// <summary>
    /// The data of a texture, contains width, height and pixels
    /// </summary>
    public struct TextureData{
        /// <summary>
        /// Width of the texture
        /// </summary>
        public int width;

        /// <summary>
        /// Height of the texture
        /// </summary>
        public int height;

        /// <summary>
        /// Pixels in bytes, RGBA format
        /// </summary>
        public byte[] pixels;

        /// <summary>
        /// Generate a texture data struct
        /// </summary>
        /// <param name="width">The texture height</param>
        /// <param name="height">The tetxure width</param>
        /// <param name="pixels">The pixels in bytes, RGBA format</param>
        public TextureData(int width, int height, byte[] pixels){
            this.width = width;
            this.height = height;
            this.pixels = pixels;
        }
    }
}