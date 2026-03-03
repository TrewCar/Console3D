using System.IO;
using System.Reflection;
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

interface ILoader
{
    public string FileReader(string filePath);
    public string FolderReader(string path);
    public List<SceneObject> ObjectReader(string v, string fileType = "JSON");
}
public struct SceneObject
{
    public Vector3 Position;
    public string Name;
    public Vector3 Size;
}
public class Loader : ILoader
{
    static public string filePath = "";
    static public string fileType = "JSON";

    public string FileReader(string filePath)
    {
        switch (fileType) {
            case "JSON": 
                return File.ReadAllText(filePath);
            default:
                throw new ArgumentException("This type (\"{0}\") is not supported.", fileType);
        }
    }

    public List<SceneObject> ObjectReader(string filePath, string fileType = "JSON")
    {
        List<SceneObject>? AllObjects = []; 
        string fileContents = FileReader(filePath);
        AllObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SceneObject>>(fileContents);
        return (AllObjects != null) ? AllObjects : [];
    }

    public string FolderReader(string path)
    {
        string[] allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        foreach (var item in allFiles)
        {
            Console.WriteLine("\t-->\t{0}", item);
        }
        return "";
    }
}