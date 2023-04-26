using System.Numerics;
using static ConsoleHelper;
using static MathVec;

static class Render3D
{
    const int Width = 120 * 2;
    const int Height = 30 * 2;
    private static float aspect = Width / Height;
    private static float aspectPixel = 11.0f / 24.0f;
    private static string Gradient = " .:!/r(l1Z4H9W8$@";
    static public void Render()
    {
        CreateBuffer(Width, Height);
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
                    Vector3 ro = new Vector3(-2, 0, 0);
                    Vector3 rd = Normalize(new Vector3(2, uv.X, uv.Y));
                    ro = RotateY(ro, 0.25f);
                    rd = RotateY(rd, 0.25f);
                    ro = RotateZ(ro, t * 0.01f);
                    rd = RotateZ(rd, t * 0.01f);
                    float diff = 1f;
                    var queue = SortAlPos(ro);
                    var objects = new List<(Vector3 pos, string name, Vector3 size)>();
                    while (queue.TryDequeue(out (Vector3 pos, string name, Vector3 size) ttt, out float tt))
                    {
                        objects.Add(ttt);
                    }

                    for (int k = 0; k < 5; k++)
                    {
                        float minIt = 99999f;
                        Vector3 n = Vector3.Zero;
                        float albedo = 1f;
                        Vector2 intersection = new();

                        for (int q = 0; q < objects.Count; q++)
                        {
                            switch (objects[q].name)
                            {
                                case "Sphere":
                                    Object.CreateSphere(ro, rd, ref intersection, ref n, ref minIt, objects[q].pos, objects[q].size);
                                    break;
                                case "Box":
                                    Object.CreateBox(ro, rd, ref intersection, ref n, ref minIt, objects[q].size, objects[q].pos);
                                    break;
                                default:
                                    break;
                            }
                        }

                        Object.CreatePlate(ro, rd, ref intersection, ref n, ref minIt, ref albedo);

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
    private static PriorityQueue<(Vector3 pos, string name, Vector3 size), float> SortAlPos(Vector3 ro)
    {
        PriorityQueue<(Vector3 pos, string name, Vector3 size), float> queue = new PriorityQueue<(Vector3 pos, string name, Vector3 size), float>();

        for (int i = 0; i < ListObjects.posAllObject.Length; i++)
        {
            queue.Enqueue(ListObjects.posAllObject[i], -1 * Vector3.Distance(ListObjects.posAllObject[i].pos, ro));
        }

        return queue;
    }
}

