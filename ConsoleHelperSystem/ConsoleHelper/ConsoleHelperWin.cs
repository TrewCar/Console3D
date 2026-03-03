using System.Runtime.InteropServices;

public class ConsoleHelperWin : WindowsDLL, IConsoleHelper
{
    private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(StandardOutputHandle);
    private static IntPtr screen;
    private static IntPtr hConsole;
    private const int STD_OUTPUT_HANDLE = -11;
    private const int FixedWidthTrueType = 54;
    private const int StandardOutputHandle = -11;
    const int GENERIC_READ = unchecked((int)0x80000000);
    const int GENERIC_WRITE = 0x40000000;
    private static int WIDHT;
    private static int HEIGHT;

    public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
    {
        FontInfo before = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>()
        };
        if (GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
        {
            FontInfo set = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = FixedWidthTrueType,
                FontName = font,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };
            if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
            {
                var ex = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(ex);
            }
            FontInfo after = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);
            return new[] { before, set, after };
        }
        else
        {
            var er = Marshal.GetLastWin32Error();
            throw new System.ComponentModel.Win32Exception(er);
        }
    }

    private static void SetWindow(int width, int height)
    {
        COORD coord = new COORD();
        coord.X = (short)width;
        coord.Y = (short)height;
        SMALL_RECT rect = new SMALL_RECT();
        rect.Top = 0;
        rect.Left = 0;
        rect.Bottom = (short)(height - 1);
        rect.Right = (short)(width - 1);
        IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
        SetConsoleScreenBufferSize(handle, coord);
        SetConsoleWindowInfo(handle, true, ref rect);
    }

    public void CreateBuffer(int widht, int height, short SizeForn)
    {
        WIDHT = widht;
        HEIGHT = height;
        SetCurrentFont("Consolas", SizeForn);
        SetWindow(WIDHT, HEIGHT);
        screen = Marshal.AllocHGlobal(WIDHT * HEIGHT);
        hConsole = CreateConsoleScreenBuffer(0x40000000, 0x00000002, IntPtr.Zero, 0x00000001, IntPtr.Zero);
        SetConsoleActiveScreenBuffer(hConsole);
    }

    public void PrintConsole(string[] screen)
    {
        PrintConsole(string.Join("", screen));
    }

    public void PrintConsole(char[] screen)
    {
        PrintConsole(new string(screen));
    }

    public void PrintConsole(string screen)
    {
        WriteConsoleOutputCharacter(hConsole, screen, (uint)(WIDHT * HEIGHT), new COORD(), out uint tr);
    }

}