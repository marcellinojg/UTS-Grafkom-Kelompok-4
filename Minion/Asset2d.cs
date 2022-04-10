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
        public void addCoordinatesToVertices(float _x, float _y)
        {
            tiga_titik[indexs * 3] = _x;
            tiga_titik[indexs * 3 + 1] = _y;
            tiga_titik[indexs * 3 + 2] = 0;
            indexs++;
            
        }
        public void CreateCurveBezier()
        {
            List<float> _vertices_bezier = new List<float>();
            List<int> pascal = getRow(indexs - 1);
            _pascal = pascal.ToArray();
            for (float t = 0; t <= 1.0f; t += 0.01f)
            {
                Vector2 p = getP(indexs, t);
                _vertices_bezier.Add(p.X);
                _vertices_bezier.Add(p.Y);
                _vertices_bezier.Add(0);
            }
            setVertices(_vertices_bezier.ToArray());
        }
        public Vector2 getP(int n, float t)
        {
            Vector2 p = new Vector2(0, 0);
            float[] k = new float[n];

            for (int i = 0; i < n; i++)
            {
                k[i] = (float)Math.Pow((1 - t), n - 1 - i) * (float)Math.Pow(t, i) * _pascal[i];
            }
            for (int i = 0; i < n; i++)
            {
                p.X += k[i] * tiga_titik[i * 3];
                p.Y += k[i] * tiga_titik[i * 3 + 1];
            }

            return p;
        }
        public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();

            //element 1 dari pascal
            currow.Add(1);

            if (rowIndex == 0)
            {
                return currow;
            }
            List<int> prev = getRow(rowIndex - 1);
            //nambah element pascal yg ditengah
            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            //nambah element yg terakhir
            currow.Add(1);

            return currow;
        }

        public void setVertices(float[] vertices)
        {
            _vertices = vertices;
        }  
    }
}
