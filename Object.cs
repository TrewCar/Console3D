using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Object : MathVec
{
    public static void CreateSphere(Vector3 ro, Vector3 rd, ref Vector2 intersection, ref Vector3 n, ref float minIt, Vector3 pos, Vector3 size)
    {
        intersection = Sphere(ro - pos, rd, size.X);
        if (intersection.X > 0f)
        {
            Vector3 itPoint = ro - pos + rd * intersection.X;
            minIt = intersection.X;
            n = Normalize(itPoint);
        }
    }
    public static void CreateBox(Vector3 ro, Vector3 rd, ref Vector2 intersection, ref Vector3 n, ref float minIt, Vector3 SizeBox, Vector3 PosBox)
    {
        Vector3 boxN = Vector3.Zero;
        intersection = Box(ro, rd, SizeBox, PosBox, out boxN);
        if (intersection.X > 0f && intersection.X < minIt)
        {
            minIt = intersection.X;
            n = boxN;
        }
    }
    public static void CreatePlate(Vector3 ro, Vector3 rd, ref Vector2 intersection, ref Vector3 n, ref float minIt, ref float albedo)
    {
        intersection = new Vector2(Plane(ro, rd, new Vector3(0f, 0f, -1f), 1f));
        if (intersection.X > 0f && intersection.X < minIt)
        {
            minIt = intersection.X;
            n = new Vector3(0f, 0f, -1f);
            albedo = 0.5f;
        }
    }
}

