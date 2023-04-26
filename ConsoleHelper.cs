using System;
using System.Runtime.InteropServices;
//Не мое, нашел в инете
public static class ConsoleHelper
{
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

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    private const int STD_OUTPUT_HANDLE = -11;

    const int GENERIC_READ = unchecked((int)0x80000000);
    const int GENERIC_WRITE = 0x40000000;
    private static int WIDHT;
    private static int HEIGHT;
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
    private static IntPtr screen;
    private static IntPtr hConsole;
    public static void CreateBuffer(int widht, int height)
    {
        WIDHT = widht;
        HEIGHT = height;
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
}
