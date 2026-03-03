using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
// namespace Vector;
// using static MathVec;

class Program
{
    // static void Main() => Render3D.Render(); 

    static void Main()
    {
        var screen = Davinci.Screen(200, 200);
        Davinci.Drawer(Davinci.Text(screen, 10, 9, " -------------- "));

        char[] text = "| Hello World! |".ToCharArray();
        for (int i = 0; i < text.Length; i++)
        {
            screen = Davinci.Text(screen, 10+i, 10, text[i]);
            Davinci.Drawer(screen);
            Thread.Sleep(text[i] == ' ' ? 0 : 250); // Пропуск пробелов в тексте
        }

        Davinci.Drawer(Davinci.Text(screen, 10, 11, " -------------- "));
    }
}