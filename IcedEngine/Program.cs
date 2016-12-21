using OpenTK;

namespace IcedEngine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (Toolkit.Init())
            {
                var testGame = new TestGame(800, 600, "Test Engine");
            }
        }
    }
}
