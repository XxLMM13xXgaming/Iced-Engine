using OpenTK;

namespace IcedEngine
{
    internal class Program
    {
        private static void Main()
        {
            using (Toolkit.Init())
            {
                new TestGame(800, 600, "Test Engine");
            }
        }
    }
}
