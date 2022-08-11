/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Text;

namespace OrkEngine3D.Resources;

public class ResourceFile
{
    private readonly string path;

    private ResourceDirectory root = new ResourceDirectory("");

    public ResourceFile(string path)
    {
        this.path = path;
    }

    public void Load()
    {
        FileStream stream = File.OpenRead(path);
        root = (ResourceDirectory)Load(stream);
    }

    private Resource Load(FileStream stream)
    {
        while (stream.Position <= stream.Length)
        {
            ResourceFlags header = (ResourceFlags)stream.ReadByte();
            if (header.HasFlag(ResourceFlags.File))
            {
                return LoadFile(stream, header);
            }
            else
            {
                return LoadDirectory(stream, header);
            }
        }

        return null;
    }

    private ResourceItem LoadFile(FileStream stream, ResourceFlags header)
    {
        ResourceItem item = new ResourceItem("");
        item.Header = header;
        int c = stream.ReadByte();
        while (c > 0)
        {
            item.Name += (char)(byte) c;

            c = stream.ReadByte();
        }

        byte[] buffer = new byte[512];
        stream.Read(buffer);
        item.DataSize = BitConverter.ToUInt32(buffer);
        item.Data[0] = LoadData(stream, item);

        return item;
    }

    private ResourceItemData LoadData(FileStream stream, ResourceItem item)
    {
        byte[] buffer = new byte[item.DataSize];
        stream.Read(buffer);

        return new ResourceItemData(buffer);
    }

    private ResourceDirectory LoadDirectory(FileStream stream, ResourceFlags header)
    {
        ResourceDirectory directory = new ResourceDirectory("");
        directory.Header = header;
        int c = stream.ReadByte();
        while (c > 0)
        {
            directory.Name += (char)(byte) c;

            c = stream.ReadByte();
        }

        byte[] buffer = new byte[512];
        stream.Read(buffer);
        directory.DataSize = BitConverter.ToUInt32(buffer);
        for (int i = 0; i < directory.DataSize; i++)
        {
            directory.Data[i] = Load(stream);
        }

        return directory;
    }
}

public abstract class Resource
{
    public ResourceFlags Header;
    public string Name;
    public uint DataSize;
    public Resource[] Data;

    public abstract byte[] GetData();
}

public class ResourceDirectory : Resource
{

    public ResourceDirectory(string name)
    {
        Name = name;
        Header = ResourceFlags.None;
        DataSize = 512;
        Data = new Resource[512];
    }
    
    public override byte[] GetData()
    {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte)Header);
        bytes.AddRange(Encoding.ASCII.GetBytes(Name));
        for (int i = 0; i < DataSize; i++)
        {
            bytes.AddRange(Data[i].GetData());
        }

        return bytes.ToArray();
    }
}

public class ResourceItem : Resource
{

    public ResourceItem(string name)
    {
        Name = name;
        Header = ResourceFlags.File;
        DataSize = 1;
        Data = new Resource[1];
    }
    
    public override byte[] GetData()
    {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte)Header);
        bytes.AddRange(Encoding.ASCII.GetBytes(Name));
        for (int i = 0; i < DataSize; i++)
        {
            bytes.AddRange(Data[i].GetData());
        }

        return bytes.ToArray();
    }
}

public class ResourceItemData : Resource
{
    private byte[] rawData;

    public ResourceItemData(byte[] data)
    {
        this.rawData = data;
    }

    public override byte[] GetData() => rawData;
}

[Flags]
public enum ResourceFlags : byte
{
    None = 0x00,
    File = 0x01
}