using System;
using OpenTK;
using OpenTK.Graphics;

namespace IcedEngine
{
    public abstract class Game : GameWindow
    {
        public static Game Instance { get; private set; }

        protected Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            if(Instance != null)
            {
                Console.WriteLine("You should never more than one game class!");
            }
            Instance = this;
            Run();
        }

        protected override void OnLoad(EventArgs e)
        {
            Initialize();
            RenderingSystem.Initialize();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Input.Update();
            Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            RenderingSystem.ClearScreen();
            Render();
            SwapBuffers();
        }

        protected override void OnClosed(EventArgs e)
        {
            Shutdown();
            Dispose();
        }

        //Virtual Methods
        protected virtual void Initialize()
        {
        
        }

        protected virtual void Update() 
        { 
        
        }

        protected virtual void Render() 
        {
        
        }

        protected virtual void Shutdown() 
        {
        
        }
    }
}
