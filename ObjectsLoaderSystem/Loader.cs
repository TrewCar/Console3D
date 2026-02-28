using System.IO;
using System.Reflection;
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

interface ILoader
{
    public string FileReader(string filePath);
    public string FolderReader(string path);
    public (Vector3 pos, string name, Vector3 size)[] ObjectReader(string v, string fileType = "JSON");
}

public class Loader : ILoader
{
    static public string filePath = "";
    static public string fileType = "JSON";

    public struct SceneObject
    {
        public Vector3 Position;
        public string Name;
        public Vector3 Size;
    }

    public string FileReader(string filePath)
    {
        
        switch (fileType) {
            case "JSON": 
                return File.ReadAllText(filePath);
            default:
                throw new ArgumentException("This type (\"{0}\") is not supported.", fileType);
                // return "[{\"position\": [0, 0, 0], \"name\": \"Box\", \"size\": [1, 1, 1]}]";
        }
    }

    public (Vector3 pos, string name, Vector3 size)[] ObjectReader(string filePath, string fileType = "JSON")
    {
        (Vector3 pos, string name, Vector3 size)[] AllObjects = []; 
        string fileContents = FileReader(filePath);
        JArray jArray = JArray.Parse(fileContents);
        SceneObject sceneObject = new SceneObject();
        foreach (var obj in jArray)
        {
            sceneObject.Position = new Vector3((int)obj["position"][0], (int)obj["position"][1], (int)obj["position"][2]);
            sceneObject.Size = new Vector3((int)obj["size"][0], (int)obj["size"][1], (int)obj["size"][2]);
            sceneObject.Name = (string)obj["name"];
            AllObjects = AllObjects.Append<(Vector3 pos, string name, Vector3 size)>((sceneObject.Position, sceneObject.Name, sceneObject.Size)).ToArray();
            Console.WriteLine("{0} {1} {2}", sceneObject.Position, sceneObject.Name, sceneObject.Size);
        }
        return AllObjects;
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