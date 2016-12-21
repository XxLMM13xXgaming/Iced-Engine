using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace IcedEngine
{
    public class RenderingSystem
    {
        public static void Initialize(float red = 0.0f, float green = 0.0f, float blue = 0.0f, float alpha = 0.0f)
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public static void SetClearColor(float red = 0.0f, float green = 0.0f, float blue = 0.0f, float alpha = 0.0f)
        {
            GL.ClearColor(red, green, blue, alpha);
        }

        public static void SetClearColor(Color color)
        {
            GL.ClearColor(color);
        }

        public static void ClearScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
