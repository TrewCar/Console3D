using System.Diagnostics.SymbolStore;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Schema;

class Davinci
{
    public static readonly int Height = Console.WindowHeight;
    public static readonly int Width = Console.WindowWidth;
    const string Gradient = " .:!/r(l1Z4H9W8$@";
    static float aspect = Width / Height;
    static float aspectPixel = 11.0f / 24.0f;
    const short SizeFont = 8;
    public static char[] screen = new string(' ', Height * Width).ToCharArray();

    public static char[] Screen(int size_x = 0, int size_y = 0)
    {
        if (size_x == 0 || size_y == 0)
        {
            char[] screen = new string(' ', Height * Width).ToCharArray();
        } 
        else
        {
            char[] screen = new string(' ', size_x * size_y).ToCharArray();
        }
        return screen;
    }

    public static void Drawer(char[] screen)
    {
        IConsoleHelper ConsoleHelper = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new ConsoleHelperWin() : new ConsoleHelperUnix();
        ConsoleHelper.CreateBuffer(Width, Height, SizeFont);
        ConsoleHelper.PrintConsole(screen);
    }

    public static char[] Text(char[] screen, int pos_x, int pos_y, char symbol)
    {
        return Text(screen, pos_x, pos_y, symbol.ToString());
    }
    public static char[] Text(char[] screen, int pos_x, int pos_y, string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            screen[(pos_x+i) + pos_y * Width] = text[i];
        }
        return screen;
    }
}