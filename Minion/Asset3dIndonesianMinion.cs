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
    internal class Asset3dIndonesianMinion
    {
        public List<Vector3> _vertices = new List<Vector3>();
        List<Vector3> _vertice_bezier = new List<Vector3>();
        int _vertexBufferObject;
        int _vertexArrayObject;
        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;
        public Shader _shader;
        public Vector3 _centerPosition;
        public List<Asset3dIndonesianMinion> children;
        public List<Vector3> _euler;

        List<uint> _indices = new List<uint>();
        public Vector3 _center;
        public Vector3 _rotateCenter;
        int _elementBufferObject;

        public Asset3dIndonesianMinion()
        {
            setDefault();
        }

        public Asset3dIndonesianMinion(List<Vector3> vertices, List<uint> indices)
        {
            _vertices = vertices;
            _indices = indices;
            setDefault();
        }

        public void setDefault()
        {
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0, 0, 0);
            children = new List<Asset3dIndonesianMinion>();
            _euler = new List<Vector3>();
            _euler.Add(new Vector3(1, 0, 0));

            _euler.Add(new Vector3(0, 1, 0));

            _euler.Add(new Vector3(0, 0, 1));
        }

        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?
            var real_angle = angle;
            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            _centerPosition = getRotationResult(pivot, vector, angle, _centerPosition);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);
            foreach (var item in children)
            {
                item.rotate(pivot, vector, real_angle);
            }
        }

        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void Load(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count *
                    sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), Size_x / (float)Size_y, 0.5f, 100.0f);
            foreach (var item in children)
            {
                item.Load(shadervert, shaderfrag, Size_x, Size_y);
            }


        }
        public void Render(Matrix4 temp, int type, Matrix4 camera_view, Matrix4 camera_projection)
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);

            _model = temp;

            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);

            if (_indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                if (type == 0)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);

                }
                else if (type == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count);
                }
                else if (type == 2)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, (_vertices.Count + 1) / 3);
                }

            }



            foreach (var item in children)
            {
                item.Render(temp, type, camera_view, camera_projection);
            }
        }
        public void createCylinder(float center_x, float center_y, float center_z, float radius, float height)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;
            Vector3 temp_vector;

            for (float deg = 0; deg <= 360; deg++)
            {
                for (float t = 0; t <= height; t += height / 2)
                {
                    temp_vector.X = center_x + radius * (float)Math.Cos(deg);
                    temp_vector.Y = center_y + t;
                    temp_vector.Z = center_z + radius * (float)Math.Sin(deg);
                    _vertices.Add(temp_vector);
                }
            }

        }
        public void createHorizontalCylinder(float center_x, float center_y, float center_z, float radius, float height)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;
            Vector3 temp_vector;
            for (float deg = 0; deg <= 360; deg += 1.5f)
            {
                for (float t = 0; t <= height; t += height / 2)
                {
                    temp_vector.Y = center_y + radius * (float)Math.Cos(deg);
                    temp_vector.X = center_x + t;
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

            for (float u = -pi / 2; u <= pi / 2; u += pi / 100)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 100)
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

            for (float u = -pi / 2; u <= pi / 2; u += pi / 100)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 100)
                {
                    temp_vector.X = center_x + radius * (float)Math.Sin(v);
                    temp_vector.Y = center_y - (radius * (float)Math.Cos(v) * (float)Math.Cos(u));
                    temp_vector.Z = center_z + radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createBottomHalfSphereElliptical(float center_x, float center_y, float center_z, float radius, float a)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi / 2; u <= pi / 2; u += pi / 360)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 360)
                {
                    temp_vector.X = center_x + a * radius * (float)Math.Sin(v);
                    temp_vector.Y = center_y - (radius * (float)Math.Cos(v) * (float)Math.Cos(u));
                    temp_vector.Z = center_z + a * radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createEllipsoid(float center_x, float center_y, float center_z, float radius_x, float radius_y, float radius_z)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 180)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 180)
                {
                    temp_vector.X = center_x + radius_x * (float)Math.Cos(v) * (float)Math.Cos(u);
                    temp_vector.Y = center_y + radius_y * (float)Math.Cos(v) * (float)Math.Sin(u);
                    temp_vector.Z = center_z + radius_z * (float)Math.Sin(v);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createTorus(float center_x, float center_y, float center_z, float r1, float r2)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = 0; u <= 2 * pi; u += pi / 300)
            {
                for (float v = 0; v <= 2 * pi; v += pi / 300)
                {
                    temp_vector.X = center_x + (r1 + r2 * (float)Math.Cos(v)) * (float)Math.Cos(u);
                    temp_vector.Y = center_y + (r1 + r2 * (float)Math.Cos(v)) * (float)Math.Sin(u);
                    temp_vector.Z = center_z + r2 * (float)Math.Sin(v);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createFrontEllipticalCylinder(float center_x, float center_y, float center_z, float radius, float height, float a)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            Vector3 temp_vector;
            for (float deg = 0; deg <= 360; deg++)
            {
                for (float t = 0; t <= height; t += height / 2)
                {
                    temp_vector.X = center_x + a * radius * (float)Math.Cos(deg);
                    temp_vector.Y = center_y + t;
                    temp_vector.Z = center_z + a * Math.Abs(radius * (float)Math.Sin(deg));
                    _vertices.Add(temp_vector);
                }
            }

        }

        public void createBackEllipticalCylinder(float center_x, float center_y, float center_z, float radius, float height, float a)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            Vector3 temp_vector;
            for (float deg = 0; deg <= 360; deg++)
            {
                for (float t = 0; t <= height; t += height / 2)
                {
                    temp_vector.X = center_x + a * radius * (float)Math.Sin(deg);
                    temp_vector.Y = center_y + t;
                    temp_vector.Z = center_z + (-a) * Math.Abs(radius * (float)Math.Cos(deg));
                    _vertices.Add(temp_vector);
                }
            }

        }

        public Vector3 getSegment(float Time)
        {
            Time = Math.Clamp(Time, 0, 1);
            float time = 1 - Time;
            Vector3 result =
            ((float)Math.Pow(time, 3) * _vertice_bezier[0])
            + (3 * time * time * Time * _vertice_bezier[1])
            + (3 * time * Time * Time * _vertice_bezier[2])
            + (Time * Time * Time * _vertice_bezier[3]);
            return result;
        }
        public void Bezier3d()
        {
            List<Vector3> segments = new List<Vector3>();
            float time;

            for (float i = 0; i < 1.0f; i += 0.01f)
            {

                time = i;
                segments.Add(getSegment(time));
            }

            setVertices(segments);

        }
        public void setVertices(List<Vector3> vertices)
        {
            _vertices = vertices;
        }
        public void AddCoordinates(float x1, float y1, float z1)
        {
            _vertice_bezier.Add(new Vector3(x1, y1, z1));

        }

        public void createBoxVertices(float x, float y, float z, float p, float l, float t)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            //Titik 1
            temp_vector.X = x + p / 2.0f;
            temp_vector.Y = y + t / 2.0f;
            temp_vector.Z = z + l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 2
            temp_vector.X = x - p / 2.0f;
            temp_vector.Y = y - t / 2.0f;
            temp_vector.Z = z - l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 3
            temp_vector.X = x + p / 2.0f;
            temp_vector.Y = y - t / 2.0f;
            temp_vector.Z = z - l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 4
            temp_vector.X = x - p / 2.0f;
            temp_vector.Y = y + t / 2.0f;
            temp_vector.Z = z - l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 5
            temp_vector.X = x - p / 2.0f;
            temp_vector.Y = y - t / 2.0f;
            temp_vector.Z = z + l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 6
            temp_vector.X = x + p / 2.0f;
            temp_vector.Y = y + t / 2.0f;
            temp_vector.Z = z - l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 7
            temp_vector.X = x + p / 2.0f;
            temp_vector.Y = y - t / 2.0f;
            temp_vector.Z = z + l / 2.0f;
            _vertices.Add(temp_vector);
            //Titik 8
            temp_vector.X = x - p / 2.0f;
            temp_vector.Y = y + t / 2.0f;
            temp_vector.Z = z + l / 2.0f;
            _vertices.Add(temp_vector);

            _indices = new List<uint>
            { 
                //Segitiga Kiri 1
                1,2,3,
                //Segitiga Kiri 2
                2,3,5,
                //Segitiga Bawah 1
                1,2,4,
                //Segitiga Bawah 2
                2,4,6,
                //Segitiga Depan 1
                1,3,4,
                //Segitiga Depan 2
                3,4,7,
                //Segitiga Belakang 1
                2,5,6,
                //Segitiga Belakang 2
                0,5,6,
                //Segitiga Atas 1
                0,3,5,
                //Segitiga Atas 2
                0,3,7,
                //Segitiga Kanan 1
                0,6,7,
                //Segitiga Kanan 2
                4,6,7
            };

        }

        //public void addChild(float x, float y, float z, float length)
        //{
        //    Asset3d newChild = new Asset3d();
        //    newChild.createBoxVertices(z, y, z, length);
        //    children.Add(newChild);
        //}
    }
}
