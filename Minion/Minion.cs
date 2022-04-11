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

        List<float> vertices_temp = new List<float>();
        Asset3d body;
        Asset3d eyeWhiteLeft;
        Asset3d eyeWhiteRight;
        Asset3d eyeLeft;
        Asset3d eyeRight;
        Asset3d glassesLeft;
        Asset3d glassesRight;
        Asset3d glassesStrap;
        Asset3d suit;
        Asset3d rightHand;
        Asset3d leftHand;
        Asset3d rightArm;
        Asset3d leftArm;
        Asset3d leftLeg;
        Asset3d rightLeg;
        Asset3d leftFoot;
        Asset3d rightFoot;

        public Asset3d hairs;
        
        public Minion()
        {

            //Eyes and Glasses
            glassesLeft = new Asset3d();
            glassesLeft.createEllipsoid(-0.22f, 0.3f, 0.4f, 0.25f, 0.25f, 0.25f);

            glassesRight = new Asset3d();
            glassesRight.createEllipsoid(0.22f, 0.3f, 0.4f, 0.25f, 0.25f, 0.25f);

            glassesStrap = new Asset3d();
            glassesStrap.createCylinder(0.0f, 0.25f, 0.0f, 0.6f, 0.15f);

            eyeWhiteLeft = new Asset3d();
            eyeWhiteLeft.createEllipsoid(-0.22f, 0.3f, 0.47f, 0.2f, 0.2f, 0.2f);

            eyeWhiteRight = new Asset3d();
            eyeWhiteRight.createEllipsoid(0.22f, 0.3f, 0.47f, 0.2f, 0.2f, 0.2f);

            eyeLeft = new Asset3d();
            eyeLeft.createEllipsoid(-0.22f, 0.3f, 0.6f, 0.1f, 0.1f, 0.1f);

            eyeLeft.children.Add(new Asset3d());
            eyeLeft.children[0].createEllipsoid(-0.22f, 0.3f, 0.67f, 0.05f, 0.05f, 0.05f);

            eyeRight = new Asset3d();
            eyeRight.createEllipsoid(0.22f, 0.3f, 0.6f,0.1f, 0.1f, 0.1f);

            eyeRight.children.Add(new Asset3d());
            eyeRight.children[0].createEllipsoid(0.22f, 0.3f, 0.67f, 0.05f, 0.05f, 0.05f);


            //Main Body and Suits
            body = new Asset3d();
            body.createCylinder(0.0f, -0.5f, 0.0f, 0.55f, 1.0f);

            body.children.Add(new Asset3d());
            body.children[0].createTopHalfSphere(0.0f, 0.51f, 0.0f, 0.55f);

            body.children.Add(new Asset3d());
            body.children[1].createBottomHalfSphere(0.0f, -0.51f, 0.0f, 0.55f);


            suit = new Asset3d();
            suit.createBottomHalfSphereElliptical(0.0f, -0.65f, 0.0f, 0.4f, 1.32f);

            suit.children.Add(new Asset3d());
            suit.children[0].createCylinder(0.0f, -0.4f, 0.0f, 0.56f, 0.1f);

            

            //Hairs
            hairs = new Asset3d();
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());

            hairs.AddCoordinates(0.0f, 1.0f, 0.0f);
            hairs.AddCoordinates(0.1f, 1.1f,0.0f);
            hairs.AddCoordinates(0.2f, 1.1f,0.0f);
            hairs.AddCoordinates(0.4f, 0.9f, 0.0f);

            hairs.children[0].AddCoordinates(0.0f, 1.0f, 0.0f);
            hairs.children[0].AddCoordinates(-0.1f, 1.1f, 0.0f);
            hairs.children[0].AddCoordinates(-0.2f, 1.1f, 0.0f);
            hairs.children[0].AddCoordinates(-0.4f, 0.9f, 0.0f);

            hairs.children[1].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[1].AddCoordinates(0.1f, 1.1f, 0.2f);
            hairs.children[1].AddCoordinates(0.2f, 1.1f, 0.2f);
            hairs.children[1].AddCoordinates(0.4f, 0.8f, 0.2f);

            hairs.children[2].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[2].AddCoordinates(-0.1f, 1.1f, 0.2f);
            hairs.children[2].AddCoordinates(-0.2f, 1.1f, 0.2f);
            hairs.children[2].AddCoordinates(-0.4f, 0.8f, 0.2f);

            hairs.children[3].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[3].AddCoordinates(0.1f, 1.1f, -0.2f);
            hairs.children[3].AddCoordinates(0.2f, 1.1f, -0.2f);
            hairs.children[3].AddCoordinates(0.4f, 0.8f, -0.2f);

            hairs.children[4].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[4].AddCoordinates(-0.1f, 1.1f, -0.2f);
            hairs.children[4].AddCoordinates(-0.2f, 1.1f, -0.2f);
            hairs.children[4].AddCoordinates(-0.4f, 0.8f, -0.2f);




            hairs.Bezier3d();
            hairs.children[0].Bezier3d();
            hairs.children[1].Bezier3d();
            hairs.children[2].Bezier3d();
            hairs.children[3].Bezier3d();
            hairs.children[4].Bezier3d();




            //Arms
            rightArm = new Asset3d();
            rightArm.createHorizontalCylinder(0.5f,-0.2f, 0.0f, 0.03f, 0.4f);

            leftArm = new Asset3d();
            leftArm.createHorizontalCylinder(-0.9f, -0.2f, 0.0f, 0.03f, 0.4f);

            
            //Hands
            leftHand = new Asset3d();
            leftHand.createEllipsoid(-0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            rightHand = new Asset3d();
            rightHand.createEllipsoid(0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            //Legs
            leftLeg = new Asset3d();
            leftLeg.createCylinder(-0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            rightLeg = new Asset3d();
            rightLeg.createCylinder(0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            //Foots
            leftFoot = new Asset3d();
            leftFoot.createEllipsoid(-0.2f, -1.2f, 0.05f,0.08f,0.08f,0.12f);

            rightFoot = new Asset3d();
            rightFoot.createEllipsoid(0.2f, -1.2f, 0.05f, 0.08f, 0.08f, 0.12f);
            
            //Fingers left
            leftHand.children.Add(new Asset3d());
            leftHand.children[0].createEllipsoid(-0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3d());
            leftHand.children[1].createEllipsoid(-0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3d());
            leftHand.children[2].createEllipsoid(-0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);

            
            //Fingers right
            rightHand.children.Add(new Asset3d());
            rightHand.children[0].createEllipsoid(0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3d());
            rightHand.children[1].createEllipsoid(0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3d());
            rightHand.children[2].createEllipsoid(0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);


            //Inheritances
            leftArm.children.Add(leftHand);
            rightArm.children.Add(rightHand);

            eyeWhiteLeft.children.Add(eyeLeft);
            eyeWhiteRight.children.Add(eyeRight);





        }


        public void Load()
        {
            glassesLeft.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            glassesRight.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            glassesStrap.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(glassesStrap._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);

            eyeWhiteLeft.Load(Constants.path + "white_eye.vert", Constants.path + "white_eye.frag", 800, 800);

            eyeWhiteRight.Load(Constants.path + "white_eye.vert", Constants.path + "white_eye.frag", 800, 800);

            body.Load(Constants.path + "minion_body.vert", Constants.path + "minion_body.frag", 800, 800);

            body.children[1].Load(Constants.path + "minion_body.vert", Constants.path + "minion_body.frag", 800, 800);

            suit.Load(Constants.path + "blue_denim.vert", Constants.path + "blue_denim.frag", 800, 800);

            suit.children[0].Load(Constants.path + "blue_denim.vert", Constants.path + "blue_denim.frag", 800, 800);

            

            eyeLeft.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(eyeLeft._shader.Handle, "ourColor"), 0.7f, 0.296f, 0.0f, 1.0f);

            eyeRight.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(eyeRight._shader.Handle, "ourColor"), 0.7f, 0.296f, 0.0f, 1.0f);

            rightArm.Load(Constants.path + "minion_body.vert", Constants.path + "minion_body.frag", 800, 800);
            

            leftArm.Load(Constants.path + "minion_body.vert", Constants.path + "minion_body.frag", 800, 800);

            
            leftHand.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(leftHand._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);

            rightHand.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(rightHand._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);


            


            leftLeg.Load(Constants.path + "blue_denim.vert", Constants.path + "blue_denim.frag", 800, 800);

            rightLeg.Load(Constants.path + "blue_denim.vert", Constants.path + "blue_denim.frag", 800, 800);

            rightFoot.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(rightFoot._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);

            leftFoot.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(leftFoot._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);

            hairs.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag",800,800);
            GL.Uniform4(GL.GetUniformLocation(hairs._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);




        }

        
        public void Render(Matrix4 temp)
        {
            body.Render(temp);
            suit.Render(temp);
            rightArm.Render(temp);

            leftArm.Render(temp);

            leftHand.Render(temp);
            rightHand.Render(temp);

            eyeWhiteLeft.Render(temp);
            eyeWhiteRight.Render(temp);
            eyeLeft.Render(temp);
            eyeRight.Render(temp);
            glassesStrap.Render(temp);
            glassesLeft.Render(temp);
            glassesRight.Render(temp);



            leftLeg.Render(temp);
            rightLeg.Render(temp);

            leftFoot.Render(temp);
            rightFoot.Render(temp);

            hairs.Render(temp);










        }   


    }
}

