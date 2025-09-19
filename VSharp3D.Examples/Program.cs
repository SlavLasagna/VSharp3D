using VSharp3D.Core;

namespace VSharp3D.Examples;

class Program
{
    static void Main()
    {
        using var engine = new Engine();
        try
        {
            engine.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
}