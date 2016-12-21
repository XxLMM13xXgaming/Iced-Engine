using OpenTK.Graphics.OpenGL4;
using System;

namespace IcedEngine
{
    public class Mesh2D
    {
        private readonly int vboID;
        private readonly int iboID;
        private readonly int size;

        public Mesh2D(Vertex[] vertices, int[] indices)
        {
            vboID = GL.GenBuffer();
            iboID = GL.GenBuffer();
            size = indices.Length;

            var data = Vertex.Process(vertices);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Draw()
        {
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.VertexAttribPointer(0, Vertex.Size, VertexAttribPointerType.Float, false, Vertex.Size * 4, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboID);
            GL.DrawElements(BeginMode.Triangles, size, DrawElementsType.UnsignedInt, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.DisableVertexAttribArray(0);
        }
    }
}
