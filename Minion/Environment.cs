using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class Environment
    {
        Asset3d platform;
        Asset3d road;
        Asset3d tree1;
        Asset3d tree2;
        Asset3d lamp1;
        Asset3d lamp2;
        Asset3d rope;
        Asset3d roadBarrier;
        Asset3d leaf1;
        Asset3d leaf2;
        Asset3d dirt1;
        Asset3d dirt2;
        Asset3d pole1;
        Asset3d pole2;
        Asset3d sun;
        float SizeX = 1200;
        float SizeY = 1200;


        public void setDefault()
        {
            platform = new Asset3d();
            rope = new Asset3d();
            pole1 = new Asset3d();
            pole2 = new Asset3d();
            road = new Asset3d();
            road.children.Add(new Asset3d());
            road.children.Add(new Asset3d());
            road.children.Add(new Asset3d());
            roadBarrier = new Asset3d();
            roadBarrier.children.Add(new Asset3d());
            lamp1 = new Asset3d();
            lamp1.children.Add(new Asset3d());
            lamp2 = new Asset3d();
            lamp2.children.Add(new Asset3d());
            dirt1 =  new Asset3d();
            dirt2 = new Asset3d();
            tree1 = new Asset3d();
            tree2 = new Asset3d();
            leaf1 = new Asset3d();
            leaf2 = new Asset3d();
            sun = new Asset3d();
        }
        public Environment()
        {
            setDefault();

            rope.AddCoordinates(3.5f, 0.5f, 1.5f);
            rope.AddCoordinates(3.0f, -0.5f, 1.5f);
            rope.AddCoordinates(2.0f, -0.5f, 1.5f);
            rope.AddCoordinates(1.5f, 0.5f, 1.5f);
            rope.Bezier3d();

            platform.createBoxVertices(0.0f, -0.8f, 0.0f, 9.0f, 5.0f, 1.0f);
            road.createBoxVertices(0.0f, -0.79f, 0.0f, 2.0f, 4.98f, 1.0f);
            road.children[0].createBoxVertices(0.0f, -0.78f, -1.5f, 0.15f, 0.6f, 1.0f);
            road.children[1].createBoxVertices(0.0f, -0.78f, 0.0f, 0.15f, 0.6f, 1.0f);
            road.children[2].createBoxVertices(0.0f, -0.78f, 1.5f, 0.15f, 0.6f, 1.0f);
            roadBarrier.createBoxVertices(1.0f, -0.6f, 0.0f, 0.15f, 4.98f, 0.8f);
            roadBarrier.children[0].createBoxVertices(-1.0f, -0.6f, 0.0f, 0.15f, 4.98f, 0.8f);


            lamp1.createCylinder(1.5f,-0.5f,0.0f,0.05f,2.5f);
            lamp1.children[0].createEllipsoid(1.5f, 2.0f, 0.0f, 0.2f, 0.2f, 0.2f);
            lamp2.createCylinder(-1.5f, -0.5f, 0.0f, 0.05f, 2.5f);
            lamp2.children[0].createEllipsoid(-1.5f, 2.0f, 0.0f, 0.2f, 0.2f, 0.2f);

            tree1.createCylinder(1.7f, -0.5f, -1.2f, 0.15f, 3.5f);
            tree2.createCylinder(-1.7f, -0.5f, 1.2f, 0.15f, 3.5f);

            leaf1.createEllipticParaboloid(1.7f, 3.1f, -1.2f, 0.5f, 0.5f, 2.0f);
            leaf2.createEllipticParaboloid(-1.7f, 3.1f, 1.2f, 0.5f, 0.5f, 2.0f);

            dirt1.createCylinder(1.7f, -0.3f, -1.2f, 0.4f, 0.05f);
            dirt2.createCylinder(-1.7f, -0.3f, 1.2f, 0.4f, 0.05f);

            leaf1.rotate(leaf1._centerPosition, Vector3.UnitX, 180f);
            leaf2.rotate(leaf2._centerPosition, Vector3.UnitX, 180f);

            sun.createEllipsoid(-2.0f, 6.0f, -6.0f, 0.8f, 0.8f, 0.8f);

            pole1.createCylinder(3.5f, -0.5f, 1.5f, 0.05f, 1.5f);
            pole2.createCylinder(1.5f, -0.5f, 1.5f, 0.05f, 1.5f);


        }

        public void Load()
        {
            platform.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(platform._shader.Handle, "ourColor"), 0.86f, 0.86f, 0.86f, 0.86f);


            road.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(road._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);

            road.children[0].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(road.children[0]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);

            road.children[1].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(road.children[1]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);

            road.children[2].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(road.children[2]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);


            lamp1.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);

            lamp2.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);

            lamp1.children[0].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(lamp1.children[0]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);

            lamp2.children[0].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(lamp2.children[0]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);

            sun.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(sun._shader.Handle, "ourColor"), 0.988f, 0.9f, 0.44f, 1.0f);

            tree1.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(tree1._shader.Handle, "ourColor"), 0.46f, 0.36f, 0.28f, 1.0f);

            tree2.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(tree2._shader.Handle, "ourColor"), 0.46f, 0.36f, 0.28f, 1.0f);

            leaf1.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(leaf1._shader.Handle, "ourColor"), 0.26f, 0.41f, 0.18f, 1.0f);

            leaf2.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(leaf2._shader.Handle, "ourColor"), 0.26f, 0.41f, 0.18f, 1.0f);

            dirt1.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(dirt1._shader.Handle, "ourColor"), 0.21f, 0.08f, 0.03f, 1.0f);

            dirt2.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(dirt2._shader.Handle, "ourColor"), 0.21f, 0.08f, 0.03f, 1.0f);

            roadBarrier.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);

            roadBarrier.children[0].Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);

            rope.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", SizeX, SizeY);

            pole1.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);
            pole2.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);





        }

        public void Render(Matrix4 _environmentModel, Matrix4 camera_view, Matrix4 camera_projection)
        {
            platform.Render(_environmentModel, 1, camera_view, camera_projection);
            road.Render(_environmentModel, 1, camera_view, camera_projection);
            road.children[0].Render(_environmentModel, 1, camera_view, camera_projection);
            road.children[1].Render(_environmentModel, 1, camera_view, camera_projection);
            road.children[2].Render(_environmentModel, 1, camera_view, camera_projection);
            roadBarrier.Render(_environmentModel, 1, camera_view, camera_projection);
            roadBarrier.children[0].Render(_environmentModel, 1, camera_view, camera_projection);


            lamp1.Render(_environmentModel, 1, camera_view, camera_projection);
            lamp2.Render(_environmentModel, 1, camera_view, camera_projection);
            lamp1.children[0].Render(_environmentModel, 0, camera_view, camera_projection);
            lamp2.children[0].Render(_environmentModel, 0, camera_view, camera_projection);
            sun.Render(_environmentModel, 0, camera_view, camera_projection);

            pole1.Render(_environmentModel, 1, camera_view, camera_projection);
            pole2.Render(_environmentModel, 1, camera_view, camera_projection);

            tree1.Render(_environmentModel, 1, camera_view, camera_projection);
            tree2.Render(_environmentModel, 1, camera_view, camera_projection);

            leaf1.Render(_environmentModel, 0, camera_view, camera_projection);
            leaf2.Render(_environmentModel, 0, camera_view, camera_projection);

            dirt1.Render(_environmentModel, 1, camera_view, camera_projection);
            dirt2.Render(_environmentModel, 1, camera_view, camera_projection);

            rope.Render(_environmentModel, 0, camera_view, camera_projection);

            animateRope();
        }

        public void animateRope()
        {
            rope._centerPosition = new Vector3(2.5f, 0.5f, 1.5f);
            rope.rotate(rope._centerPosition, Vector3.UnitX, 12f);
        }
    }
}
