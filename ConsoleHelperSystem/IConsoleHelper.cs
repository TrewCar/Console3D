using System.Runtime.InteropServices;

interface IConsoleHelper
{
    public void CreateBuffer(int widht, int height, short SizeForn);
    public void PrintConsole(string[] screen);
    public void PrintConsole(char[] screen);
    public void PrintConsole(string screen);
}

public class WindowsDLL
{
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

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern IntPtr GetStdHandle(int nStdHandle);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    protected static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [DllImport("kernel32")]
    protected extern static bool SetConsoleFont(IntPtr hOutput, uint index);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    protected static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern IntPtr CreateConsoleScreenBuffer(uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwFlags, IntPtr lpScreenBufferData);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);
  
    [DllImport("Kernel32.dll")]
    protected static extern IntPtr CreateConsoleScreenBuffer(
        int dwDesiredAccess, int dwShareMode,
        IntPtr secutiryAttributes,
        UInt32 flags,
        IntPtr screenBufferData);

    [DllImport("kernel32.dll")]
    protected static extern bool WriteConsole(
        IntPtr hConsoleOutput, string lpBuffer,
        uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten,
        IntPtr lpReserved);
}