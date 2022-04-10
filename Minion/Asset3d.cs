using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    static class Constants
    {
        public const string path = "../../../Shaders/";
    }
    internal class Asset3d
    {
        List<Vector3> _vertices = new List<Vector3>();
        List<float> _vertices_for_bezier= new List<float>();
        
        List<uint> _indices = new List<uint>();
        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;
        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;
        public Shader _shader;
        public Vector3 _centerPosition;
        public List<Asset3d> children;
        int indexs;
        int[] _pascal = { };

        public Asset3d()
        {
            setDefault();
        }

        public void setDefault()
        {
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0, 0, 0);
            children = new List<Asset3d>();
            indexs = 0;
        }

        public void Load(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes, _vertices.ToArray(),BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.5f, 100.0f);
            foreach (var item in children)
            {
                item.Load(shadervert,shaderfrag,Size_x,Size_y);
            }

        }

        public void Render(Matrix4 temp)
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            _model = temp;

            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", _view);
            _shader.SetMatrix4("projection", _projection);

            GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);

            foreach(var item in children)
            {
                item.Render(temp);
            }
        }

        public void createCylinder(float center_x, float center_y, float center_z, float radius, float height)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for(float t = 0; t <= height; t+=0.001f)
            {
                for(float deg = 0; deg <= 360; deg++)
                {
                    temp_vector.X = center_x + radius * (float) Math.Cos(deg);
                    temp_vector.Y = center_y + t;
                    temp_vector.Z = center_z + radius * (float)Math.Sin(deg);
                    _vertices.Add(temp_vector);
                }
            }

        }

        public void createTopHalfSphere(float center_x, float center_y, float center_z, float radius)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi/2; u <= pi/2; u += pi / 1200)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 1200)
                {
                    temp_vector.X = center_x + radius * (float)Math.Sin(v);
                    temp_vector.Y = center_y + radius * (float)Math.Cos(v) * (float)Math.Cos(u);
                    temp_vector.Z = center_z + radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createBottomHalfSphere(float center_x, float center_y, float center_z, float radius)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi / 2; u <= pi / 2; u += pi / 1200)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 1200)
                {
                    temp_vector.X = center_x + radius * (float)Math.Sin(v);
                    temp_vector.Y = center_y -(radius * (float)Math.Cos(v) * (float)Math.Cos(u));
                    temp_vector.Z = center_z + radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    _vertices.Add(temp_vector);
                }
            }
        }


        public void addCoordinatesToVertices(float x, float y, float z)
        {
            _vertices_for_bezier.Add(x);
            _vertices_for_bezier.Add(y);
            _vertices_for_bezier.Add(z);
            indexs++;
        }
        public List<float> createCurveBezier()
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

            return _vertices_bezier;
        }

        public Vector2 getP(int n, float t)
        {
            Vector2 p = new Vector2(0, 0);
            float[] k = new float[n];
            for (int i = 0; i < n; i++)
            {
                k[i] = (float)Math.Pow((1 - t), n - 1 - i) * (float)Math.Pow(t, i) * _pascal[i];
                p.X += k[i] * _vertices_for_bezier[i * 3];
                p.Y += k[i] * _vertices_for_bezier[i * 3 + 1];
            }
            return p;
        }
        public List<int> getRow(int rowIndex)
        {
            List<int> curRow = new List<int>();
            curRow.Add(1);
            if (rowIndex == 0)
            {
                return curRow;
            }
            List<int> prev = getRow(rowIndex - 1);
            for (int i = 1; 1 < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                curRow.Add(curr);
            }
            curRow.Add(1);
            return curRow;
        }

        public void createEllipsoid(float center_x, float center_y, float center_z, float radius_x, float radius_y, float radius_z)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for(float u = -pi; u <= pi; u += pi/300)
            {
                for(float v = -pi/2; v <= pi/2; v += pi/300)
                {
                    temp_vector.X = center_x + radius_x * (float)Math.Cos(v) * (float) Math.Cos(u);
                    temp_vector.Y = center_y + radius_y * (float)Math.Cos(v) * (float)Math.Sin(u);
                    temp_vector.Z = center_z + radius_z * (float)Math.Sin(v);
                    _vertices.Add(temp_vector);
                }
            }
        }

    }
}

    
