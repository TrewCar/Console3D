using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class MathVec
{
    #region Math
    public static float Clamp(float value, float min, float max) => MathF.Max(Math.Min(value, max), min);
    public static int Clamp(int value, int min, int max) => Math.Max(Math.Min(value, max), min);
    public static float Sign(float a) => Math.Sign(a);
        
    public static float Lenght(Vector2 v) => MathF.Sqrt(v.X * v.X + v.Y * v.Y);
    public static float Lenght(Vector3 v) => MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    public static Vector3 Norm(Vector3 v) => v / Lenght(v);
    public static Vector3 Abs(Vector3 v) => new Vector3(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
    public static Vector3 Reflect(Vector3 rd, Vector3 n) => rd - n * (2 * Dot(n, rd));
    public static Vector3 Normalize(Vector3 v)
    {
        return v / v.Length();
    }
    public static Vector2 Vec3ToVec2(Vector3 v) => new Vector2(v.X, v.Z);

    // Calculate the dot product of two 3D vectors
    public static float Dot(Vector3 a, Vector3 b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }
    public static Vector3 RotateX(Vector3 a, float angle)
    {
        Vector3 b = a;
        b.Z = a.Z * MathF.Cos(angle) - a.Y * MathF.Sin(angle);
        b.Y = a.Z * MathF.Sin(angle) + a.Y * MathF.Cos(angle);
        return b;
    }

    public static Vector3 RotateY(Vector3 a, float angle)
    {
        Vector3 b = a;
        b.X = a.X * MathF.Cos(angle) - a.Z * MathF.Sin(angle);
        b.Z = a.X * MathF.Sin(angle) + a.Z * MathF.Cos(angle);
        return b;
    }

    public static Vector3 RotateZ(Vector3 a, float angle)
    {
        Vector3 b = a;
        b.X = a.X * MathF.Cos(angle) - a.Y * MathF.Sin(angle);
        b.Y = a.X * MathF.Sin(angle) + a.Y * MathF.Cos(angle);
        return b;
    }

    protected static Vector2 Sphere(Vector3 ro, Vector3 rd, float r)
    {
        float b = Vector3.Dot(ro, rd);
        float c = ro.LengthSquared() - r * r;
        float h = b * b - c;
        if (h < 0.0f)
        {
            return new Vector2(-1.0f);
        }
        h = (float)Math.Sqrt(h);
        return new Vector2(-b - h, -b + h);
    }
    static protected Vector2 Box(Vector3 ro, Vector3 rd, Vector3 boxSize, Vector3 boxPos, out Vector3 outNormal)
    {
        Vector3 relPos = ro - boxPos;
        Vector3 m = new Vector3(1.0f) / rd;
        Vector3 n = m * relPos;
        Vector3 k = Vector3.Abs(m) * boxSize;
        Vector3 t1 = -n - k;
        Vector3 t2 = -n + k;
        float tN = Math.Max(Math.Max(t1.X, t1.Y), t1.Z);
        float tF = Math.Min(Math.Min(t2.X, t2.Y), t2.Z);
        if (tN > tF || tF < 0.0f)
        {
            outNormal = Vector3.Zero;
            return new Vector2(-1.0f);
        }
        Vector3 yzx = new Vector3(t1.Y, t1.Z, t1.X);
        Vector3 zxy = new Vector3(t1.Z, t1.X, t1.Y);
        outNormal = -Sign(rd) * step(yzx, t1) * step(zxy, t1);
        return new Vector2(tN, tF);
    }
    protected static float Plane(Vector3 ro, Vector3 rd, Vector3 p, float w)
    {
        return -(Vector3.Dot(ro, p) + w) / Vector3.Dot(rd, p);
    }
    public static Vector3 Sign(Vector3 v) =>
        new Vector3(Math.Sign(v.X), Math.Sign(v.Y), Math.Sign(v.Z));
    public static Vector3 step(Vector3 edge, Vector3 x)
    {
        return new Vector3(step(edge.X, x.X), step(edge.Y, x.Y), step(edge.Z, x.Z));
    }
    public static float step(float edge, float x)
    {
        return x < edge ? 0.0f : 1.0f;
    }
    #endregion

        
}
