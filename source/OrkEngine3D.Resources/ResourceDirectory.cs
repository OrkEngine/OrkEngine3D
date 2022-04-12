using System.Text;

namespace OrkEngine3D.Resources;

public class ResourceDirectory
{
    private string path;
    private List<string> dictionaries = new List<string>();
    private Dictionary<string, int> files = new Dictionary<string, int>();
    public ResourceDirectory(string path)
    {
        this.path = path;
    }

    public void Add(string file) => files.Add(file);

    public void Load()
    {
        FileStream stream = File.OpenRead(path);
        if (stream.ReadByte() != 0x00)
            throw new IOException("Cannot read non-directory file as directory file!");
        List<byte> cstring = new List<byte>();
        files.Clear();
        dictionaries.Clear();
        
        for (int i = 0; i < stream.Read; i++)
        {
            
        }

        if(stream.ReadByte() == 0x00)
                dictionaries.Add(Encoding.ASCII.GetString(cstring.ToArray()));
            else
                stream.Position--;

            cstring.Add((byte)stream.ReadByte());
        }
        
    }

    private byte[] getBytes()
    {
        List<byte> bytes = new List<byte>();
        bytes.Add(0x00);

        for (int i = 0; i < files.Count; i++)
        {
            bytes.AddRange(Encoding.ASCII.GetBytes(files[i]));
            bytes.Add(0x00);
        }

        return bytes.ToArray();
    }
    
    public void Save()
    {
        File.WriteAllBytes(path, getBytes());
    }

    public List<string> GetFiles()
    {
        return files;
    }
}