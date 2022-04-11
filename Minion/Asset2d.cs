using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace Minion
{
    internal class Asset2d
    {
        float[] _vertices;
        float[] tiga_titik = new float[9];
        
        int _vertexBufferObject;
        int _vertexArrayObject;
        public Shader _shader;
        int indexs = 0;
        int[] _pascal = { };
        List<Asset2d> children = new List<Asset2d>();

        public Asset2d(float[] vertices)
        {
            _vertices = vertices;
        }

        public Asset2d()
        {
            
        }

        public void Load(string shadervert, string shaderfrag)
        {

            //Buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();
            foreach (var item in children)
            {
                item.Load(shadervert, shaderfrag);
            }

        }

        public void Render()
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, indexs);

            foreach (var item in children)
            {
                item.Render();
            }

        }
        

        

        
    }
}
