using System.Text;

public class ConsoleHelperUnix : IConsoleHelper
{
    private static int WIDTH;
    private static int HEIGHT;
    private static StringBuilder? screenBuffer;
    private static Stream stdout = Console.OpenStandardOutput();

    private const string ENTER_ALT_BUFFER = "\x1b[?1049h";
    private const string EXIT_ALT_BUFFER  = "\x1b[?1049l";
    private const string HIDE_CURSOR      = "\x1b[?25l";
    private const string SHOW_CURSOR      = "\x1b[?25h";
    private const string CLEAR            = "\x1b[2J";
    private const string MOVE_HOME        = "\x1b[H";

    public void CreateBuffer(int width, int height, short SizeForn = 8)
    {
        WIDTH = width;
        HEIGHT = height;

        screenBuffer = new StringBuilder(width * height);

        WriteRaw(ENTER_ALT_BUFFER);
        WriteRaw(HIDE_CURSOR);
        WriteRaw(CLEAR);
    }

    public void PrintConsole(string[] screen)
    {
        PrintConsole(string.Join("", screen));
    }

    public void PrintConsole(char[] screen)
    {
        PrintConsole(new string(screen));
    }

    public void PrintConsole(string buffer)
    {
        if (buffer.Length != WIDTH * HEIGHT)
            throw new ArgumentException("Buffer size mismatch");

        // Перемещаем курсор в начало
        WriteRaw(MOVE_HOME);

        // Один write — ключевая оптимизация
        WriteRaw(buffer);
    }

    public static void Dispose()
    {
        WriteRaw(SHOW_CURSOR);
        WriteRaw(EXIT_ALT_BUFFER);
    }

    private static void WriteRaw(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        stdout.Write(bytes, 0, bytes.Length);
        stdout.Flush();
    }
}