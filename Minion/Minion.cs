using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class Minion
    {
        Asset3d body;
        Asset3d eyeWhiteLeft;
        Asset3d eyeWhiteRight;
        Asset3d eyeLeft;
        Asset3d eyeRight;
        Asset3d glassesLeft;
        Asset3d glassesRight;
        Asset3d glassesStrap;
        public Minion()
        {
            glassesLeft = new Asset3d();
            glassesLeft.createEllipsoid(-0.22f, 0.3f, 0.5f, 0.25f, 0.25f, 0.25f);

            glassesRight = new Asset3d();
            glassesRight.createEllipsoid(0.22f, 0.3f, 0.5f, 0.25f, 0.25f, 0.25f);

            glassesStrap = new Asset3d();
            glassesStrap.createCylinder(0.0f, 0.18f, 0.0f, 0.6f, 0.2f);

            eyeWhiteLeft = new Asset3d();
            eyeWhiteLeft.createEllipsoid(-0.22f, 0.3f, 0.5f, 0.2f, 0.2f, 0.2f);

            eyeWhiteRight = new Asset3d();
            eyeWhiteRight.createEllipsoid(0.22f, 0.3f, 0.5f, 0.2f, 0.2f, 0.2f);

            eyeLeft = new Asset3d();
            eyeLeft.createEllipsoid(-0.22f, 0.3f, 0.63f, 0.1f, 0.1f, 0.1f);

            eyeLeft.children.Add(new Asset3d());
            eyeLeft.children[0].createEllipsoid(-0.22f, 0.3f, 0.63f, 0.05f, 0.05f, 0.05f);

            eyeRight = new Asset3d();
            eyeRight.createEllipsoid(0.22f, 0.3f, 0.63f,0.1f, 0.1f, 0.1f);

            eyeRight.children.Add(new Asset3d());
            eyeRight.children[0].createEllipsoid(0.22f, 0.3f, 0.63f, 0.05f, 0.05f, 0.05f);

            body = new Asset3d();
            body.createCylinder(0.0f, -0.5f, 0.0f, 0.55f, 1.0f);

            body.children.Add(new Asset3d());
            body.children[0].createTopHalfSphere(0.0f, 0.51f, 0.0f, 0.55f);

            body.children.Add(new Asset3d());
            body.children[1].createBottomHalfSphere(0.0f, -0.51f, 0.0f, 0.55f);

            eyeWhiteLeft.children.Add(eyeLeft);
            eyeWhiteRight.children.Add(eyeRight);

            


        }


        public void Load()
        {
            glassesLeft.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            glassesRight.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            glassesStrap.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            eyeWhiteLeft.Load(Constants.path + "white_eye.vert", Constants.path + "white_eye.frag", 800, 800);

            eyeWhiteRight.Load(Constants.path + "white_eye.vert", Constants.path + "white_eye.frag", 800, 800);

            body.Load(Constants.path + "minion_body.vert", Constants.path + "minion_body.frag", 800, 800);

            eyeLeft.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(eyeLeft._shader.Handle, "ourColor"), 0.7f, 0.296f, 0.0f, 1.0f);

            eyeRight.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(eyeRight._shader.Handle, "ourColor"), 0.7f, 0.296f, 0.0f, 1.0f);



        }
        public void Render(Matrix4 temp)
        {
            body.Render(temp);
            glassesLeft.Render(temp);
            glassesRight.Render(temp);
            glassesStrap.Render(temp);
            eyeWhiteLeft.Render(temp);
            eyeWhiteRight.Render(temp);
            eyeLeft.Render(temp);
            eyeRight.Render(temp);
            
            
            




        }   


    }
}

