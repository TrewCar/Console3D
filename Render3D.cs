using System.Numerics;
using static ConsoleHelper;
using static MathVec;

static class Render3D
{
    const int Width = 120*2;
    const int Height = 30*2;
    const short SizeFont = 8;
    const float speed = 0.05f;
    const int Light = 1;
    private static float aspect = Width / Height;
    private static float aspectPixel = 11.0f / 24.0f;
    private static string Gradient = " .:!/r(l1Z4H9W8$@";
    static public void Render()
    {
        CreateBuffer(Width, Height, SizeFont);
        var obj = ListObjects.posAllObject;
        long t = 0;
        int len = Gradient.Length - 1;
        while (true)
        {
            Vector3 light = Normalize(new Vector3((float)Math.Cos(t * 0.001f), (float)Math.Sin(t * 0.001f), -1.0f));
            t += 1;
            char[] screen = new string(' ', Height * Width).ToCharArray();

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Vector2 uv = new Vector2(i, j) / new Vector2(Width, Height) * 2.0f - Vector2.One;
                    uv.X *= aspect * aspectPixel;
                    Vector3 ro = new Vector3(-14, 0, 0);
                    Vector3 rd = Normalize(new Vector3(2, uv.X, uv.Y));
                    ro = RotateY(ro, 0.25f);
                    rd = RotateY(rd, 0.25f);
                    ro = RotateZ(ro, t * speed);
                    rd = RotateZ(rd, t * speed);
                    ro = RotateY(ro, t * speed);
                    rd = RotateY(rd, t * speed);
                    ro = RotateX(ro, t * speed);
                    rd = RotateX(rd, t * speed);
                    float diff = 1f;
                    Array.Sort(obj, (a,b) => Vector3.Distance(b.pos,ro).CompareTo(Vector3.Distance(a.pos, ro)));

                    for (int k = 0; k < Light; k++)
                    {
                        float minIt = 99999f;
                        Vector3 n = Vector3.Zero;
                        float albedo = 1f;
                        Vector2 intersection = new();

                        for (int q = 0; q < obj.Length; q++)
                        {
                            switch (obj[q].name)
                            {
                                case "Sphere":
                                    Object.CreateSphere(ro, rd, ref intersection, ref n, ref minIt, obj[q].pos, obj[q].size);
                                    break;
                                case "Box":
                                    Object.CreateBox(ro, rd, ref intersection, ref n, ref minIt, obj[q].size, obj[q].pos);
                                    break;
                                default:
                                    break;
                            }
                        }

                        //Object.CreatePlate(ro, rd, ref intersection, ref n, ref minIt, ref albedo);

                        if (minIt < 99999f)
                        {
                            diff *= (Dot(n, light) * 0.5f + 0.5f) * albedo;
                            ro = ro + rd * (minIt - 0.01f);
                            rd = Reflect(rd, n);
                        }
                        else break;
                    }
                    int color = (int)(diff * 20f);
                    color = Clamp(color, 0, Gradient.Length - 1);
                    char pixel = Gradient[color];
                    screen[i + j * Width] = pixel;
                }
            }
            PrintConsole(screen);
        }
    }
}

