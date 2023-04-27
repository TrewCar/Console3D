using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class ListObjects
{
    static public (Vector3 pos, string name, Vector3 size)[] posAllObject = new (Vector3 pos, string name, Vector3 size)[]
    {
        (new Vector3(0, 3, 0), "Sphere",new Vector3(1)),       
        (new Vector3(3, 0, 0), "Sphere",new Vector3(1)),
        (new Vector3(-3, 0, 0), "Sphere",new Vector3(1)),
        (new Vector3(3, 3, 0), "Sphere",new Vector3(1)),
        (new Vector3(-3, -3, 0), "Sphere",new Vector3(1)),
        (new Vector3(3, -3, 0), "Sphere",new Vector3(1)),
        (new Vector3(-3, 3, 0), "Sphere",new Vector3(1)),
        (new Vector3(0, -3, 0), "Sphere",new Vector3(1)),
        
        (new Vector3(0, 3, -3), "Sphere",new Vector3(1)),
        (new Vector3(3, 0, -3), "Sphere",new Vector3(1)),
        (new Vector3(-3, 0, -3), "Sphere",new Vector3(1)),
        (new Vector3(3, 3, -3), "Sphere",new Vector3(1)),
        (new Vector3(-3, -3, -3), "Sphere",new Vector3(1)),
        (new Vector3(3, -3, -3), "Sphere",new Vector3(1)),
        (new Vector3(-3, 3, -3), "Sphere",new Vector3(1)),
        (new Vector3(0, -3, -3), "Sphere",new Vector3(1)),
        (new Vector3(0,0,-3), "Sphere", new Vector3(1)),

        (new Vector3(0, 3, 3), "Sphere",new Vector3(1)),
        (new Vector3(3, 0, 3), "Sphere",new Vector3(1)),
        (new Vector3(-3, 0, 3), "Sphere",new Vector3(1)),
        (new Vector3(3, 3, 3), "Sphere",new Vector3(1)),
        (new Vector3(-3, -3, 3), "Sphere",new Vector3(1)),
        (new Vector3(3, -3, 3), "Sphere",new Vector3(1)),
        (new Vector3(-3, 3, 3), "Sphere",new Vector3(1)),
        (new Vector3(0, -3, 3), "Sphere",new Vector3(1)),
        (new Vector3(0,0,3), "Sphere", new Vector3(1)),

        (new Vector3(0,0,0), "Box", new Vector3(1)),
    };
}

