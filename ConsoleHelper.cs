using System;
using System.Runtime.InteropServices;
//Не мое, нашел в инете
public static class ConsoleHelper
{
    #region strucr
    private enum StdHandle
    {
        OutputHandle = -11
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FontInfo
    {
        internal int cbSize;
        internal int FontIndex;
        internal short FontWidth;
        public short FontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.wc, SizeConst = 32)]
        public string FontName;
    }
    #endregion
    #region ImportDLL
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetStdHandle(int nStdHandle);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [DllImport("kernel32")]
    private extern static bool SetConsoleFont(IntPtr hOutput, uint index);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr CreateConsoleScreenBuffer(uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwFlags, IntPtr lpScreenBufferData);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);
  
    [DllImport("Kernel32.dll")]
    private static extern IntPtr CreateConsoleScreenBuffer(
        int dwDesiredAccess, int dwShareMode,
        IntPtr secutiryAttributes,
        UInt32 flags,
        IntPtr screenBufferData);

    [DllImport("kernel32.dll")]
    static extern bool WriteConsole(
        IntPtr hConsoleOutput, string lpBuffer,
        uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten,
        IntPtr lpReserved);
    #endregion
    #region const
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
    #endregion
    public static void CreateBuffer(int widht, int height, short SizeForn = 8)
    {
        WIDHT = widht;
        HEIGHT = height;
        SetCurrentFont("Consolas", SizeForn);
        SetWindow(WIDHT, HEIGHT);
        screen = Marshal.AllocHGlobal(WIDHT * HEIGHT);
        hConsole = CreateConsoleScreenBuffer(0x40000000, 0x00000002, IntPtr.Zero, 0x00000001, IntPtr.Zero);
        SetConsoleActiveScreenBuffer(hConsole);
    }
    public static void PrintConsole(string[] screen)
    {
        PrintConsole(string.Join("", screen));
    }
    public static void PrintConsole(char[] screen)
    {
        PrintConsole(new string(screen));
    }
    public static void PrintConsole(string screen)
    {
        WriteConsoleOutputCharacter(hConsole, screen, (uint)(WIDHT * HEIGHT), new COORD(), out uint tr);
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
}