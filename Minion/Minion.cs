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
        Asset3d eyeWhite;
        Asset3d eye;
        Asset3d glasses;
        Asset3d glassesStrap;
        Asset3d mouth;
        Asset3d tooth;
        Asset3d suit;
        Asset3d frontSuit;
        Asset3d backSuit;
        Asset3d leftStrap;
        Asset3d rightStrap;
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
            glasses = new Asset3d();
            glasses.createTorus(0.0f, 0.34f, 0.55f, 0.2f, 0.05f);

            glassesStrap = new Asset3d();
            glassesStrap.createCylinder(0.0f, 0.25f, 0.0f, 0.6f, 0.15f);

            eyeWhite = new Asset3d();
            eyeWhite.createEllipsoid(0.0f, 0.34f, 0.465f, 0.2f, 0.2f, 0.2f);

            eye = new Asset3d();
            eye.createEllipsoid(0.0f, 0.34f, 0.575f,0.1f, 0.1f, 0.1f);

            eye.children.Add(new Asset3d());
            eye.children[0].createEllipsoid(0.0f, 0.34f, 0.628f, 0.05f, 0.05f, 0.05f);

            //Mouth
            mouth = new Asset3d();
            mouth.AddCoordinates(-0.3f, 0.0f, 0.555f);
            mouth.AddCoordinates(-0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.3f, 0.0f, 0.555f);
            mouth.Bezier3d();

            tooth = new Asset3d();
            tooth.AddCoordinates(-0.15f, 0.0f, 0.568f);
            tooth.AddCoordinates(-0.1f, -0.1f, 0.568f);
            tooth.AddCoordinates(-0.05f, -0.1f, 0.568f);
            tooth.AddCoordinates(0.0f, 0.0f, 0.568f);
            
            tooth.children.Add(new Asset3d());
            tooth.children[0].AddCoordinates(0.0f, 0.0f, 0.568f);
            tooth.children[0].AddCoordinates(0.05f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.1f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.15f, 0.0f, 0.568f);

            tooth.Bezier3d();
            tooth.children[0].Bezier3d();


            //Main Body and Suits
            body = new Asset3d();
            body.createCylinder(0.0f, -0.5f, 0.0f, 0.55f, 1.0f);

            body.children.Add(new Asset3d());
            body.children[0].createTopHalfSphere(0.0f, 0.50f, 0.0f, 0.55f);

            body.children.Add(new Asset3d());
            body.children[1].createBottomHalfSphere(0.0f, -0.50f, 0.0f, 0.55f);

            suit = new Asset3d();
            suit.createBottomHalfSphereElliptical(0.0f, -0.663f, 0.0f, 0.4f, 1.32f);

            frontSuit = new Asset3d();
            frontSuit.createFrontEllipticalCylinder(0.0f,-0.663f,0.18f,0.2f,0.4f,2.0f);

            backSuit = new Asset3d();
            backSuit.createBackEllipticalCylinder(0.0f, -0.663f, -0.18f, 0.2f, 0.4f, 2.0f);

            leftStrap = new Asset3d();
            leftStrap.AddCoordinates(-0.2f, -0.2f, 0.2f);
            leftStrap.AddCoordinates(0.2f, 0.0f, 0.1f);
            leftStrap.AddCoordinates(0.2f, 0.0f, 0.1f);
            leftStrap.AddCoordinates(-0.2f, -0.2f, 0.2f);
            leftStrap.Bezier3d();


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

            eyeWhite.children.Add(eye);

        }


        public void Load()
        {
            //Glasses
            glasses.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", 800, 800);
            glassesStrap.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            //Eyes
            eyeWhite.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            eye.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", 800, 800);
            eye.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            //Mouth
            mouth.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(mouth._shader.Handle, "ourColor"), 1.0f, 0.31f, 0.32f, 1.0f);
            tooth.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);

            //Body & Suit
            body.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);

            body.children[1].Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);

            suit.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            frontSuit.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            backSuit.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            leftStrap.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            //Arms
            rightArm.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);

            leftArm.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);
            
            //Hands
            leftHand.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            rightHand.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            //Legs
            leftLeg.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            rightLeg.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            rightFoot.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            leftFoot.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            //Hairs
            hairs.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800,800);

        }

        
        public void Render(Matrix4 temp)
        {
            //Main Body & Suit
            body.Render(temp);
            suit.Render(temp);
            frontSuit.Render(temp);
            backSuit.Render(temp);
            //leftStrap.Render(temp);

            //Arms & Hands
            rightArm.Render(temp);
            leftArm.Render(temp);
            leftHand.Render(temp);
            rightHand.Render(temp);

            //Eye & Glasses
            eyeWhite.Render(temp);
            eye.Render(temp);
            glassesStrap.Render(temp);
            glasses.Render(temp);

            //Mouth
            mouth.Render(temp);
            tooth.Render(temp);

            //Legs & Foot
            leftLeg.Render(temp);
            rightLeg.Render(temp);
            leftFoot.Render(temp);
            rightFoot.Render(temp);

            //Hairs
            hairs.Render(temp);

        }


    }
}

