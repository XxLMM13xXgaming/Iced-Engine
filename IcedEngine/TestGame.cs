using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace IcedEngine
{
    public class TestGame : Game
    {
        public TestGame(int width, int height, string title) : base(width, height, title) { }

        private Mesh2D mesh2d;
        private Shader shader;
        private Transform transform;

        protected override void Initialize()
        {
            RenderingSystem.SetClearColor(Color.CornflowerBlue);
            transform = new Transform();
            shader = new Shader("Resources/Shaders/vertex.shader", "Resources/Shaders/fragment.shader");
            shader.AddUniform("transformationMatrix");

            var vertices = new Vertex[]
            {
                new Vertex(-0.5f, 0.5f),
                new Vertex(-0.5f, -0.5f),
                new Vertex(0.5f, -0.5f),
                new Vertex(0.5f, 0.5f)
            };

            var indices = new int[]
            {
                0, 1, 3,
                3, 1, 2
            };

            mesh2d = new Mesh2D(vertices, indices);
        }

        protected override void Update()
        {
            if (Input.GetKey(OpenTK.Input.Key.Left))
            {
                transform.Translate(-0.001f, 0);
                //transform.Rotate(-0.01f);
                //transform.Scale(-0.001f);
            }

            if (Input.GetKey(OpenTK.Input.Key.Right))
            {
                transform.Translate(0.001f, 0);
                //transform.Rotate(0.01f);
                //transform.Scale(0.001f);
            }
        }

        protected override void Render()
        {
            shader.Start();
            shader.LoadMatrix("transformationMatrix", transform.TransformationMatrix);
            mesh2d.Draw();
            shader.Stop();
        }
    }
}
