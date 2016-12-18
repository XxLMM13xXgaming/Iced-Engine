using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace IcedEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Toolkit.Init())
            {
                new TestGame(800, 600, "Test Engine");
            }
        }
    }
}
