using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class ListObjects
{
    public struct SceneObject
    {
        public Vector3 Position;
        public string Name;
        public Vector3 Size;
    }

    static public (Vector3 pos, string name, Vector3 size)[] posAllObject_ = {};
    public static (Vector3 pos, string name, Vector3 size)[] method()
    {
        string filePath = "./objects/example.json";
        string fileContents = File.ReadAllText(filePath);
        JArray jArray = JArray.Parse(fileContents);
        SceneObject sceneObject = new SceneObject();
        foreach (var obj in jArray)
        {
            sceneObject.Position = new Vector3((int)obj["position"][0], (int)obj["position"][1], (int)obj["position"][2]);
            sceneObject.Size = new Vector3((int)obj["size"][0], (int)obj["size"][1], (int)obj["size"][2]);
            sceneObject.Name = (string)obj["name"];
            posAllObject_ = posAllObject_.Append<(Vector3 pos, string name, Vector3 size)>((sceneObject.Position, sceneObject.Name, sceneObject.Size)).ToArray();
            Console.WriteLine("{0} {1} {2}", sceneObject.Position, sceneObject.Name, sceneObject.Size);
        }
        return posAllObject_;
    }
    
}

