using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace IcedEngine
{
    public class Shader
    {
        private readonly int programID;
        private readonly Dictionary<string, int> uniforms;

        public Shader(string vertexFileName = null, string fragmentFileName = null)
        {
            programID = GL.CreateProgram();
            uniforms = new Dictionary<string, int>();

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
            var shader = ReadShader(fileName);
            var shaderID = GL.CreateShader(type);

            if(shaderID == 0)
            {
                Console.WriteLine("Error creating shader: Could not generate shader buffer!");
                Console.ReadLine();
                Environment.Exit(1);
            }

            GL.ShaderSource(shaderID, shader);
            GL.CompileShader(shaderID);
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int compileStatus);
            
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
            GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out int linkStatus);

            if(linkStatus == 0)
            {
                Console.WriteLine("Error linking shader program: Could not link shader program!");
                Console.WriteLine(GL.GetProgramInfoLog(programID));
                Console.ReadLine();
                Environment.Exit(1);
            }

            GL.ValidateProgram(programID);
            GL.GetProgram(programID, GetProgramParameterName.ValidateStatus, out int validationStatus);
            if (validationStatus != 0) return;
            Console.WriteLine("Error validating shader program: Could not validate shader program!");
            Console.WriteLine(GL.GetProgramInfoLog(programID));
            Console.ReadLine();
            Environment.Exit(1);
        }

        public void Start()
        {
            GL.UseProgram(programID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void AddUniform(string uniformName)
        {
            var uniform = GetUniformLocation(uniformName);
            if(uniform == -1)
            {
                Console.WriteLine("Could not find uniform: " + uniformName + "!");
                Console.ReadLine();
                Environment.Exit(1);
            }

            uniforms.Add(uniformName, uniform);
        }

        private int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(programID, uniformName);
        }

        #region Uniform Loading
        public void LoadInt(string uniformName, int value)
        {
            GL.Uniform1(uniforms[uniformName], value);
        }

        public void LoadFloat(string uniformName, int value)
        {
            GL.Uniform1(uniforms[uniformName], value);
        }

        public void LoadDouble(string uniformName, int value)
        {
            GL.Uniform1(uniforms[uniformName], value);
        }

        public void LoadVector(string uniformName, Vector2 value)
        {
            GL.Uniform2(uniforms[uniformName], value);
        }

        public void LoadVector(string uniformName, Vector3 value)
        {
            GL.Uniform3(uniforms[uniformName], value);
        }

        public void LoadVector(string uniformName, Vector4 value)
        {
            GL.Uniform4(uniforms[uniformName], value);
        }

        public void LoadBoolean(string uniformName, bool value)
        {
            GL.Uniform1(uniforms[uniformName], value ? 1 : 0);
        }

        public void LoadMatrix(string uniformName, Matrix4 value)
        {
            GL.UniformMatrix4(uniforms[uniformName], true, ref value);
        }
        #endregion

        private static string ReadShader(string fileName)
        {
            var shader = new StringBuilder();

            try
            {
                using (var reader = new StreamReader(fileName))
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
