using System.Drawing;
using System.Numerics;
#if WINDOWS
using static ConsoleHelper;
#elif UNIX
using static ConsoleHelperUnix;
#endif
namespace Vector;
using static MathVec;
class Program
{
    static void Main() => Render3D.Render(); 

}