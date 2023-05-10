using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using OrkEngine3D.Diagnostics.Logging;
using StbImageSharp;

namespace OrkEngine3D.Graphics.TK.Resources
{
    // A helper class, much like Shader, meant to simplify loading textures.
    public class Texture
    {
        public readonly int Handle;

        public static Texture LoadFromFile(string path)
        {
            // Generate handle
            int handle = GL.GenTexture();

            // Bind the handle
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            // For this example, we're going to use .NET's built-in System.Drawing library to load textures.

            // OpenGL has it's texture origin in the lower left corner instead of the top left corner,
            // so we tell StbImageSharp to flip the image when loading.
            StbImage.stbi_set_flip_vertically_on_load(1);

            // Here we open a stream to the file and pass it to StbImageSharp to load.
            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                // Now that our pixels are prepared, it's time to generate a texture. We do this with GL.TexImage2D.
                // Arguments:
                //   The type of texture we're generating. There are various different types of textures, but the only one we need right now is Texture2D.
                //   Level of detail. We can use this to start from a smaller mipmap (if we want), but we don't need to do that, so leave it at 0.
                //   Target format of the pixels. This is the format OpenGL will store our image with.
                //   Width of the image
                //   Height of the image.
                //   Border of the image. This must always be 0; it's a legacy parameter that Khronos never got rid of.
                //   The format of the pixels, explained above. Since we loaded the pixels as RGBA earlier, we need to use PixelFormat.Rgba.
                //   Data type of the pixels.
                //   And finally, the actual pixels.
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            // Now that our texture is loaded, we can set a few settings to affect how the image appears on rendering.

            // First, we set the min and mag filter. These are used for when the texture is scaled down and up, respectively.
            // Here, we use Linear for both. This means that OpenGL will try to blend pixels, meaning that textures scaled too far will look blurred.
            // You could also use (amongst other options) Nearest, which just grabs the nearest pixel, which makes the texture look pixelated if scaled too far.
            // NOTE: The default settings for both of these are LinearMipmap. If you leave these as default but don't generate mipmaps,
            // your image will fail to render at all (usually resulting in pure black instead).
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Now, set the wrapping mode. S is for the X axis, and T is for the Y axis.
            // We set this to Repeat so that textures will repeat when wrapped. Not demonstrated here since the texture coordinates exactly match
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Next, generate mipmaps.
            // Mipmaps are smaller copies of the texture, scaled down. Each mipmap level is half the size of the previous one
            // Generated mipmaps go all the way down to just one pixel.
            // OpenGL will automatically switch between mipmaps when an object gets sufficiently far away.
            // This prevents moiré effects, as well as saving on texture bandwidth.
            // Here you can see and read about the morié effect https://en.wikipedia.org/wiki/Moir%C3%A9_pattern
            // Here is an example of mips in action https://en.wikipedia.org/wiki/File:Mipmap_Aliasing_Comparison.png
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }

        // Activate texture
        // Multiple textures can be bound, if your shader needs more than just one.
        // If you want to do that, use GL.ActiveTexture to set which slot GL.BindTexture binds to.
        // The OpenGL standard requires that there be at least 16, but there can be more depending on your graphics card.
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
    /*
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
                Logger.Get("TextureBinder", "Graphics").Log(LogMessageType.FATAL, "Texture index may only be 0-31");
            GL.ActiveTexture((TextureUnit)Enum.Parse(typeof(TextureUnit), "Texture"+id.ToString()));
            GL.BindTexture(TextureTarget.Texture2D, this.id);
        }

        /// <summary>
        /// OnUnload the texture
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
            image.Mutate(x => x.Flip(FlipMode.Horizontal).Flip(FlipMode.Vertical));

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
    */
}