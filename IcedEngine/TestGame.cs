﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcedEngine
{
    public class TestGame : Game
    {
        public TestGame(int width, int height, string title) : base(width, height, title) { }

        private Mesh2D mesh2d;
        private Shader shader;

        protected override void Initialize()
        {
            Vertex[] vertices = new Vertex[]
            {
                new Vertex(-1f, -1f),
                new Vertex(1f, -1f),
                new Vertex(0f, 1f)
            };

            mesh2d = new Mesh2D(vertices);
            shader = new Shader("Resources/Shader/vertex.shader");
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Render()
        {
            shader.Start();
            mesh2d.Draw();
            shader.Stop();
        }

        protected override void Shutdown()
        {
            base.Shutdown();
        }
    }
}
