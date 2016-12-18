using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcedEngine
{
    public class Input
    {
        //Key Lists
        private static List<Key> currentKeys = new List<Key>();
        private static List<Key> downKeys = new List<Key>();
        private static List<Key> upKeys = new List<Key>();

        //Mouse Lists
        private static List<MouseButton> currentMouseButtons = new List<MouseButton>();
        private static List<MouseButton> downMouseButtons = new List<MouseButton>();
        private static List<MouseButton> upMouseButtons = new List<MouseButton>();

        internal static void Update()
        {
            //Keyboard Loops
            downKeys.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(Key)).Length; i++)
            {
                if(GetKey((Key)i) && !currentKeys.Contains((Key)i))
                {
                    downKeys.Add((Key)i);
                }
            }

            upKeys.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(Key)).Length; i++)
            {
                if(!GetKey((Key)i) && currentKeys.Contains((Key)i))
                {
                    upKeys.Add((Key)i);
                }
            }

            currentKeys.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(Key)).Length; i++)
            {
                if(GetKey((Key)i))
                {
                    currentKeys.Add((Key)i);
                }
            }

            //Mouse Loops
            downMouseButtons.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(MouseButton)).Length; i++)
            {
                if (GetMouseButton((MouseButton)i) && !currentMouseButtons.Contains((MouseButton)i))
                {
                    downMouseButtons.Add((MouseButton)i);
                }
            }

            upMouseButtons.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(MouseButton)).Length; i++)
            {
                if (!GetMouseButton((MouseButton)i) && currentMouseButtons.Contains((MouseButton)i))
                {
                    upMouseButtons.Add((MouseButton)i);
                }
            }

            currentMouseButtons.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(MouseButton)).Length; i++)
            {
                if (GetMouseButton((MouseButton)i))
                {
                    currentMouseButtons.Add((MouseButton)i);
                }
            }
        }

        //Keyboard Functions
        public static bool GetKey(Key keyCode)
        {
            if(!Game.Instance.Focused)
            {
                return false;
            }

            return Keyboard.GetState().IsKeyDown((short)keyCode);
        }

        public static bool GetKeyDown(Key keyCode)
        {
            if (!Game.Instance.Focused)
            {
                return false;
            }

            return downKeys.Contains(keyCode);
        }

        public static bool GetKeyUp(Key keyCode)
        {
            if (!Game.Instance.Focused)
            {
                return false;
            }

            return upKeys.Contains(keyCode);
        }

        //Mouse Functions
        public static bool GetMouseButton(MouseButton mouseButton)
        {
            if (!Game.Instance.Focused)
            {
                return false;
            }

            return Mouse.GetState().IsButtonDown(mouseButton);
        }

        public static bool GetMouseButtonDown(MouseButton mouseButton)
        {
            if (!Game.Instance.Focused)
            {
                return false;
            }

            return downMouseButtons.Contains(mouseButton);
        }

        public static bool GetMouseButtonUp(MouseButton mouseButton)
        {
            if (!Game.Instance.Focused)
            {
                return false;
            }

            return upMouseButtons.Contains(mouseButton);
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public static void SetMousePosition(Vector2 position)
        {
            Mouse.SetPosition(position.X, position.Y);
        }

        public static void SetMousePosition(float x, float y)
        {
            Mouse.SetPosition(x, y);
        }

        public static void ShowCursor(bool visibility)
        {
            Game.Instance.CursorVisible = visibility;
        }
    }
}
