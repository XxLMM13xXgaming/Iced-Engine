using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace IcedEngine
{
    public class Shader
    {
        private int programID;

        public Shader(string vertexFileName = null, string fragmentFileName = null)
        {
            programID = GL.CreateProgram();

            if(programID == 0)
            {
                Console.WriteLine("Error creating shader: Could not generate program buffer!");
                Console.ReadLine();
                Environment.Exit(1);
            }

            if(vertexFileName != null)
            {
                AddShader(vertexFileName, ShaderType.VertexShader);
            }

            if(fragmentFileName != null)
            {
                AddShader(fragmentFileName, ShaderType.FragmentShader);
            }

            if(vertexFileName != null || fragmentFileName != null)
            {
                CompileShader();
            }
        }

        private void AddShader(string fileName, ShaderType type)
        {
            string shader = ReadShader(fileName);
            int shaderID = GL.CreateShader(type);

            if(shaderID == 0)
            {
                Console.WriteLine("Error creating shader: Could not generate shader buffer!");
                Console.ReadLine();
                Environment.Exit(1);
            }

            GL.ShaderSource(shaderID, shader);
            GL.CompileShader(shaderID);

            int compileStatus;
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out compileStatus);
            
            if(compileStatus == 0)
            {
                Console.WriteLine("Error compiling shader: Could not compile shader!");
                Console.WriteLine(GL.GetShaderInfoLog(shaderID));
                Console.ReadLine();
                Environment.Exit(1);
            }

            GL.AttachShader(programID, shaderID);
        }

        private void CompileShader()
        {
            GL.LinkProgram(programID);

            int linkStatus;
            GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out linkStatus);

            if(linkStatus == 0)
            {
                Console.WriteLine("Error linking shader program: Could not link shader program!");
                Console.WriteLine(GL.GetProgramInfoLog(programID));
                Console.ReadLine();
                Environment.Exit(1);
            }

            GL.ValidateProgram(programID);
            int validationStatus;
            GL.GetProgram(programID, GetProgramParameterName.ValidateStatus, out validationStatus);
            if(validationStatus == 0)
            {
                Console.WriteLine("Error validating shader program: Could not validate shader program!");
                Console.WriteLine(GL.GetProgramInfoLog(programID));
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public void Start()
        {
            GL.UseProgram(programID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        private static string ReadShader(string fileName)
        {
            StringBuilder shader = new StringBuilder();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        shader.AppendLine(line).Append("\n");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }

            return shader.ToString();
        }
    }
}
