using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class LifeguardMinion
    {
        List<float> vertices_temp = new List<float>();
        Asset3dLifeguardMinion body;
        Asset3dLifeguardMinion eyeWhite;
        Asset3dLifeguardMinion eyeRight;
        Asset3dLifeguardMinion eyeLeft;
        Asset3dLifeguardMinion glasses;
        Asset3dLifeguardMinion glassesStrap;
        Asset3dLifeguardMinion mouth;
        Asset3dLifeguardMinion tooth;
        Asset3dLifeguardMinion suit;
        Asset3dLifeguardMinion frontSuit;
        Asset3dLifeguardMinion backSuit;
        Asset3dLifeguardMinion leftStrap;
        Asset3dLifeguardMinion rightStrap;
        Asset3dLifeguardMinion rightHand;
        Asset3dLifeguardMinion leftHand;
        Asset3dLifeguardMinion rightArm;
        Asset3dLifeguardMinion leftArm;
        Asset3dLifeguardMinion leftLeg;
        Asset3dLifeguardMinion rightLeg;
        Asset3dLifeguardMinion leftFoot;
        Asset3dLifeguardMinion rightFoot;
        Asset3dLifeguardMinion buoy;
        int timer = 0;

        public Asset3dLifeguardMinion hairs;

        public LifeguardMinion()
        {

            //Eyes and Glasses
            glasses = new Asset3dLifeguardMinion();
            glasses.createTorus(-0.2f, 0.34f, 0.55f, 0.2f, 0.05f);

            glasses.children.Add(new Asset3dLifeguardMinion());
            glasses.children[0].createTorus(0.2f, 0.34f, 0.55f, 0.2f, 0.05f);

            glassesStrap = new Asset3dLifeguardMinion();
            glassesStrap.createCylinder(0.0f, 0.25f, -0.05f, 0.6f, 0.15f);

            //glassesStrap.children.Add(new Asset3dLifeguardMinion());
            //glassesStrap.children[0].createCylinder(0.2f, 0.25f, 0.0f, 0.6f, 0.15f);

            eyeWhite = new Asset3dLifeguardMinion();
            eyeWhite.createEllipsoid(-0.2f, 0.34f, 0.465f, 0.2f, 0.2f, 0.2f);

            eyeWhite.children.Add(new Asset3dLifeguardMinion());
            eyeWhite.children[0].createEllipsoid(0.2f, 0.34f, 0.465f, 0.2f, 0.2f, 0.2f);

            eyeRight = new Asset3dLifeguardMinion();
            eyeRight.createEllipsoid(-0.2f, 0.34f, 0.575f, 0.1f, 0.1f, 0.1f);

            eyeRight.children.Add(new Asset3dLifeguardMinion());
            eyeRight.children[0].createEllipsoid(-0.2f, 0.34f, 0.628f, 0.05f, 0.05f, 0.05f);

            eyeLeft = new Asset3dLifeguardMinion();
            eyeLeft.createEllipsoid(0.2f, 0.34f, 0.575f, 0.1f, 0.1f, 0.1f);

            eyeLeft.children.Add(new Asset3dLifeguardMinion());
            eyeLeft.children[0].createEllipsoid(0.2f, 0.34f, 0.628f, 0.05f, 0.05f, 0.05f);


            //Mouth
            mouth = new Asset3dLifeguardMinion();
            mouth.AddCoordinates(-0.3f, 0.0f, 0.555f);
            mouth.AddCoordinates(-0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.3f, 0.0f, 0.555f);
            mouth.Bezier3d();

            tooth = new Asset3dLifeguardMinion();
            tooth.AddCoordinates(-0.15f, 0.0f, 0.568f);
            tooth.AddCoordinates(-0.1f, -0.1f, 0.568f);
            tooth.AddCoordinates(-0.05f, -0.1f, 0.568f);
            tooth.AddCoordinates(0.0f, 0.0f, 0.568f);

            tooth.children.Add(new Asset3dLifeguardMinion());
            tooth.children[0].AddCoordinates(0.0f, 0.0f, 0.568f);
            tooth.children[0].AddCoordinates(0.05f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.1f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.15f, 0.0f, 0.568f);

            tooth.Bezier3d();
            tooth.children[0].Bezier3d();


            //Main Body and Suits
            body = new Asset3dLifeguardMinion();
            body.createCylinder(0.0f, -0.5f, 0.0f, 0.55f, 1.0f);

            body.children.Add(new Asset3dLifeguardMinion());
            body.children[0].createTopHalfSphere(0.0f, 0.50f, 0.0f, 0.55f);

            body.children.Add(new Asset3dLifeguardMinion());
            body.children[1].createBottomHalfSphere(0.0f, -0.50f, 0.0f, 0.55f);

            suit = new Asset3dLifeguardMinion();
            suit.createBottomHalfSphereElliptical(0.0f, -0.663f, 0.0f, 0.4f, 1.32f);

            //Buoy
            buoy = new Asset3dLifeguardMinion();
            buoy.createTorusRotateX(0.0f, -0.55f, 0.0f, 0.5f, 0.2f, 1.5f);

            buoy.children.Add(new Asset3dLifeguardMinion());
            buoy.children[0].createTorusRotateY(0.497f, -0.55f, 0.0f, 0.15f, 0.1f, 0.0f, -0.9f);

            buoy.children.Add(new Asset3dLifeguardMinion());
            buoy.children[1].createTorusRotateY(-0.497f, -0.55f, 0.0f, 0.15f, 0.1f, 0.0f, -0.9f);

            buoy.children.Add(new Asset3dLifeguardMinion());
            buoy.children[2].createTorusRotateY(0.0f, -0.52f, 0.5f, 0.15f, 0.1f, 1.3f, -0.9f);

            buoy.children.Add(new Asset3dLifeguardMinion());
            buoy.children[3].createTorusRotateY(0.0f, -0.57f, -0.5f, 0.15f, 0.1f, 1.3f, -0.9f);

            //frontSuit = new Asset3dLifeguardMinion();
            //frontSuit.createFrontEllipticalCylinder(0.0f, -0.663f, 0.18f, 0.2f, 0.4f, 2.0f);

            //backSuit = new Asset3dLifeguardMinion();
            //backSuit.createBackEllipticalCylinder(0.0f, -0.663f, -0.18f, 0.2f, 0.4f, 2.0f);

            leftStrap = new Asset3dLifeguardMinion();
            leftStrap.AddCoordinates(-0.2f, -0.2f, 0.2f);
            leftStrap.AddCoordinates(0.2f, 0.0f, 0.1f);
            leftStrap.AddCoordinates(0.2f, 0.0f, 0.1f);
            leftStrap.AddCoordinates(-0.2f, -0.2f, 0.2f);
            leftStrap.Bezier3d();


            //Hairs
            hairs = new Asset3dLifeguardMinion();
            hairs.children.Add(new Asset3dLifeguardMinion());
            hairs.children.Add(new Asset3dLifeguardMinion());
            hairs.children.Add(new Asset3dLifeguardMinion());
            hairs.children.Add(new Asset3dLifeguardMinion());
            hairs.children.Add(new Asset3dLifeguardMinion());

            hairs.AddCoordinates(0.0f, 1.0f, 0.0f);
            hairs.AddCoordinates(0.1f, 1.1f, 0.0f);
            hairs.AddCoordinates(0.2f, 1.1f, 0.0f);
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
            rightArm = new Asset3dLifeguardMinion();
            rightArm.createHorizontalCylinder(0.5f, -0.2f, 0.0f, 0.03f, 0.4f);

            leftArm = new Asset3dLifeguardMinion();
            leftArm.createHorizontalCylinder(-0.9f, -0.2f, 0.0f, 0.03f, 0.4f);

            //Hands
            leftHand = new Asset3dLifeguardMinion();
            leftHand.createEllipsoid(-0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            rightHand = new Asset3dLifeguardMinion();
            rightHand.createEllipsoid(0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            //Legs
            leftLeg = new Asset3dLifeguardMinion();
            leftLeg.createCylinder(-0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            rightLeg = new Asset3dLifeguardMinion();
            rightLeg.createCylinder(0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            //Foots
            leftFoot = new Asset3dLifeguardMinion();
            leftFoot.createEllipsoid(-0.2f, -1.2f, 0.05f, 0.08f, 0.08f, 0.12f);

            rightFoot = new Asset3dLifeguardMinion();
            rightFoot.createEllipsoid(0.2f, -1.2f, 0.05f, 0.08f, 0.08f, 0.12f);

            //Fingers left
            leftHand.children.Add(new Asset3dLifeguardMinion());
            leftHand.children[0].createEllipsoid(-0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3dLifeguardMinion());
            leftHand.children[1].createEllipsoid(-0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3dLifeguardMinion());
            leftHand.children[2].createEllipsoid(-0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);

            //Fingers right
            rightHand.children.Add(new Asset3dLifeguardMinion());
            rightHand.children[0].createEllipsoid(0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3dLifeguardMinion());
            rightHand.children[1].createEllipsoid(0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3dLifeguardMinion());
            rightHand.children[2].createEllipsoid(0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);

            //Inheritances
            leftArm.children.Add(leftHand);
            rightArm.children.Add(rightHand);

            eyeWhite.children.Add(eyeRight);

        }


        public void Load()
        {
            //Glasses
            glasses.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", 800, 800);
            glasses.children[0].Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", 800, 800);
            glassesStrap.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);
            //glassesStrap.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);



            //Eyes
            eyeWhite.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            eyeWhite.children[0].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            eyeRight.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", 800, 800);
            eyeRight.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);
            eyeLeft.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", 800, 800);
            eyeLeft.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);


            //Mouth
            mouth.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
            GL.Uniform4(GL.GetUniformLocation(mouth._shader.Handle, "ourColor"), 1.0f, 0.31f, 0.32f, 1.0f);
            tooth.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);

            //Body & Suit
            body.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);

            body.children[1].Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", 800, 800);

            suit.Load(Constants.path + "ocean_boat_blue.vert", Constants.path + "ocean_boat_blue.frag", 800, 800);

            //Buoy
            buoy.Load(Constants.path + "buoy.vert", Constants.path + "buoy.frag", 800, 800);

            buoy.children[0].Load(Constants.path + "buoyStrip.vert", Constants.path + "buoyStrip.frag", 800, 800);

            buoy.children[1].Load(Constants.path + "buoyStrip.vert", Constants.path + "buoyStrip.frag", 800, 800);

            buoy.children[2].Load(Constants.path + "buoyStrip.vert", Constants.path + "buoyStrip.frag", 800, 800);

            buoy.children[3].Load(Constants.path + "buoyStrip.vert", Constants.path + "buoyStrip.frag", 800, 800);

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
            hairs.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

        }


        public void Render(Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        {
            //Main Body & Suit
            body.Render(temp, 1, camera_view, camera_projection);
            suit.Render(temp, 0, camera_view, camera_projection);

            //Buoy
            buoy.Render(temp, 1, camera_view, camera_projection);

            //Arms & Hands
            rightArm.Render(temp, 1, camera_view, camera_projection);
            leftArm.Render(temp, 1, camera_view, camera_projection);
            leftHand.Render(temp, 0, camera_view, camera_projection);
            rightHand.Render(temp, 0, camera_view, camera_projection);

            //Eye & Glasses
            eyeWhite.Render(temp, 0, camera_view, camera_projection);
            eyeRight.Render(temp, 0, camera_view, camera_projection);
            eyeLeft.Render(temp, 0, camera_view, camera_projection);
            glassesStrap.Render(temp, 1, camera_view, camera_projection);
            glasses.Render(temp, 0, camera_view, camera_projection);

            //Mouth
            mouth.Render(temp, 1, camera_view, camera_projection);
            tooth.Render(temp, 1, camera_view, camera_projection);

            //Legs & Foot
            leftLeg.Render(temp, 1, camera_view, camera_projection);
            rightLeg.Render(temp, 1, camera_view, camera_projection);
            leftFoot.Render(temp, 0, camera_view, camera_projection);
            rightFoot.Render(temp, 0, camera_view, camera_projection);

            //Hairs
            hairs.Render(temp, 0, camera_view, camera_projection);

        }

        public Matrix4 autoJump(Matrix4 model)
        {
            if (timer >= 0 && timer < 15)
            {
                model = model * Matrix4.CreateTranslation(0.0f, 0.013f, 0.0f);
            }
            if (timer >= 15 && timer < 29)
            {
                model = model * Matrix4.CreateTranslation(0.0f, -0.013f, 0.0f);
            }
            if (timer == 29)
            {
                timer = 0;
            }
            timer++;
            return model;
        }
    }
}
