using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Shaders
{
    public sealed class ShaderCore : IBindable
    {
        private GL _openGL;
        private ShaderHandler shaderHandler;

        public uint ShaderHandle { get; private set; }

        private readonly string _vertexPath;
        private readonly string _fragmentPath;

        public ShaderCore(GL openGL, string vertexPath, string fragmentPath)
        {
            _openGL = openGL;
            ShaderHandle = _openGL.CreateProgram();

            _vertexPath = vertexPath;
            _fragmentPath = fragmentPath;
        }


        public static ShaderCore CreateDefaultShader(GL openGL) =>
            new(openGL, @"..\..\..\Shaders\VertexShader.vert", @"..\..\..\Shaders\FragmentShader.frag");

        private async Task<uint> GetShaderHandleAsync(string path, ShaderType shaderType)
        {
            var source = File.ReadAllText(path);
            return VertexHelper.CreateShaderPointer(_openGL, shaderType, source);
        }

        public unsafe void SetValue<T>(string uniform, T value)
        {
            int uniformLocation = _openGL.GetUniformLocation(ShaderHandle, uniform);
            if (uniformLocation == -1)
                throw new Exception($"Uniform {uniform} was not found");

            if (value is Matrix4x4 matrixValue)
                _openGL.UniformMatrix4(uniformLocation, 1, false, (float*)&matrixValue);
            if (value is Vector3 vectorValue)
                _openGL.Uniform3(uniformLocation, vectorValue);

            if (value is int intValue)
                _openGL.Uniform1(uniformLocation, intValue);
            if (value is double doubleValue)
                _openGL.Uniform1(uniformLocation, doubleValue);
            if (value is float floatValue)
                _openGL.Uniform1(uniformLocation, floatValue);
        }

        public async Task BindAsync()
        {
            shaderHandler = new
                (
                    await GetShaderHandleAsync(_vertexPath, ShaderType.VertexShader),
                    await GetShaderHandleAsync(_fragmentPath, ShaderType.FragmentShader)
                );

            unsafe 
            {
            _openGL.AttachShader(ShaderHandle, shaderHandler.VertexHandle);
            _openGL.AttachShader(ShaderHandle, shaderHandler.FragmentHandle);
            _openGL.LinkProgram(ShaderHandle);
            _openGL.UseProgram(ShaderHandle);

            DetachShaders(shaderHandler.VertexHandle, shaderHandler.FragmentHandle);
            _openGL.EnableVertexAttribArray(0);
            }
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
