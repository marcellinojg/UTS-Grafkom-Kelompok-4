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
        public List<Vector3> _vertices = new List<Vector3>();
        public List<uint> _indices = new List<uint>();
        List<Vector3> _vertice_bezier = new List<Vector3>();
        int _vertexBufferObject;
        int _vertexArrayObject;
        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;
        public Shader _shader;
        public Vector3 _centerPosition;
        public List<Asset3d> children;
        public List<Vector3> _euler;
  

        public Asset3d()
        {
            setDefault();
        }

        public void setDefault()
        {
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0, 0, 0);
            children = new List<Asset3d>();
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

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), Size_x / (float)Size_y, 0.5f, 100.0f);
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

            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Count);

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

        public void createHorizontalCylinder(float center_x, float center_y, float center_z, float radius, float height)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float t = 0; t <= height; t += 0.001f)
            {
                for (float deg = 0; deg <= 360; deg++)
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

        public void createBottomHalfSphereElliptical(float center_x, float center_y, float center_z, float radius, float a)
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
            Console.WriteLine("Done");
            Console.WriteLine(vertices[0]);
        }
        public void AddCoordinates(float x1, float y1, float z1)
        {
            _vertice_bezier.Add(new Vector3(x1, y1, z1));

        }

    }
}

    
