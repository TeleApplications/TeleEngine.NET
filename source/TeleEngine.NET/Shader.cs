using Silk.NET.OpenGL;
using System.Numerics;
using System.Reflection;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET
{
    internal readonly struct ShaderHandler 
    {
        public uint VertexHandle { get; }
        public uint FragmentHandle { get; }

        public ShaderHandler(uint vertex, uint fragment) 
        {
            VertexHandle = vertex;
            FragmentHandle = fragment;
        }
    }

    public sealed class Shader : IBindable
    {
        private GL _openGL;
        private ShaderHandler shaderHandler;

        public uint ShaderHandle { get; private set; }

        private readonly string _vertexPath;
        private readonly string _fragmentPath;

        public Shader(GL openGL, string vertexPath, string fragmentPath) 
        {
            _openGL = openGL;
            ShaderHandle = _openGL.CreateProgram();

            _vertexPath = vertexPath;
            _fragmentPath = fragmentPath;
        }


        public static Shader CreateDefaultShader(GL openGL) =>
            new(openGL, @"..\..\..\Shaders\VertexShader.vert", @"..\..\..\Shaders\FragmentShader.frag");

        private async Task<uint> GetShaderHandleAsync(string path, ShaderType shaderType) 
        {
            var source = File.ReadAllText(path);
            return VertexHelper.CreateShaderPointer(_openGL, shaderType, source);
        }

        private static string CreateProperPath(string path) 
        {
            var assembleLocation = Assembly.GetExecutingAssembly().Location;
            string directoryName = Path.GetDirectoryName(assembleLocation)!;

            return $@"{directoryName}\{path}";
        }

        public unsafe void SetValue<T>(string uniform, T value) 
        {
            int uniformLocation = _openGL.GetUniformLocation(ShaderHandle, uniform);
            if (uniformLocation == -1)
                throw new Exception($"Uniform {uniform} was not found");

            if (value is Vector3 || value is Matrix4x4)
            {
                var currentMatrix = value is Vector3 vectorValue ? Matrix4x4.CreateTranslation(vectorValue) : (Matrix4x4)(object)value;
                _openGL.UniformMatrix4(uniformLocation, 1, false, (float*)(&currentMatrix));
            }

            if(value is int intValue)
                _openGL.Uniform1(uniformLocation, intValue);
            if(value is double doubleValue)
                _openGL.Uniform1(uniformLocation, doubleValue);
            if(value is float floatValue)
                _openGL.Uniform1(uniformLocation, floatValue);
        }

        public unsafe async Task BindAsync() 
        {
            shaderHandler = new
                (
                    GetShaderHandleAsync(_vertexPath, ShaderType.VertexShader).Result,
                    GetShaderHandleAsync(_fragmentPath, ShaderType.FragmentShader).Result
                );

            _openGL.AttachShader(ShaderHandle, shaderHandler.VertexHandle);
            _openGL.AttachShader(ShaderHandle, shaderHandler.FragmentHandle);
            _openGL.LinkProgram(ShaderHandle);
            _openGL.UseProgram(ShaderHandle);

            DetachShaders(shaderHandler.VertexHandle, shaderHandler.FragmentHandle);
            _openGL.EnableVertexAttribArray(0);
        }

        public void DetachShaders(params uint[] shaderHandles) 
        {
            for (int i = 0; i < shaderHandles.Length; i++)
            {
                var currentShaderHandle = shaderHandles[i];
                _openGL.DetachShader(ShaderHandle, currentShaderHandle);
            }
        }
    }
}
