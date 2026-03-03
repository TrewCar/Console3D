using System.Diagnostics.SymbolStore;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Schema;

class Circle
{
    static readonly int Height = Console.WindowHeight;
    static readonly int Width = Console.WindowWidth;
    const string Gradient = " .:!/r(l1Z4H9W8$@";
    static float aspect = Width / Height;
    static float aspectPixel = 11.0f / 24.0f;
    const short SizeFont = 8;

    public static void Drawer()
    {
        char[] screen = new string(' ', Height * Width).ToCharArray();
        int Clamp(int value, int min, int max) => Math.Max(Math.Min(value, max), min);
        IConsoleHelper ConsoleHelper = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new ConsoleHelperWin() : new ConsoleHelperUnix();
        ConsoleHelper.CreateBuffer(Width, Height, SizeFont);

        float diff = 1f;
        float d = 0;
        while(true)
        {
            screen = new string(' ', Height * Width).ToCharArray();
            d+=1f / 1444f;
            float xx = Width/2 + MathF.Sin(d) * 20;
            float yy = Height/2 + MathF.Cos(d) * 20;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    int color = (int)(diff * 20f);
                    color = Clamp(color, 0, Gradient.Length - 1);
                    char pixel = Gradient[color];
                    if (((Math.Pow(i - xx, 2) * aspect * aspectPixel) + (Math.Pow(j - yy, 2) * aspect / aspectPixel)) <= Math.Pow(5,2))
                    {
                        screen[i + j * Width] = pixel;
                    }

                }
            }
            ConsoleHelper.PrintConsole(screen);
        }
    }
}