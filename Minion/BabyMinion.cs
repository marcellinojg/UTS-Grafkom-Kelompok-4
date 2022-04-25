using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class BabyMinion
    {
        public Asset3d body;
        public float SizeX = 1200;
        public float SizeY = 1200;
        Asset3d eyeWhite;
        Asset3d eye;
        Asset3d glasses;
        Asset3d glassesStrap;
        Asset3d mouth;
        Asset3d tooth;
        Asset3d diaper;
        Asset3d rightHand;
        Asset3d leftHand;
        Asset3d rightArm;
        Asset3d leftArm;
        Asset3d leftLeg;
        Asset3d rightLeg;
        Asset3d leftFoot;
        Asset3d rightFoot;
        public Asset3d hairs;
        int timer = 0;

        List<Object> rotatorEye = new List<Object>();
        List<Object> rotatorRightArm = new List<Object>();
        List<Object> rotatorLeftArm = new List<Object>();
        List<Object> rotatorWalkZ = new List<Object>();
        List<Object> rotatorWalkX = new List<Object>();

        public void setDefault()
        {
            rotatorEye.Add(true);
            rotatorEye.Add(true);
            rotatorEye.Add(0f);

            rotatorRightArm.Add(true);
            rotatorRightArm.Add(0f);

            rotatorLeftArm.Add(true);
            rotatorLeftArm.Add(0f);

            rotatorWalkZ.Add(true);
            rotatorWalkZ.Add(0f);

            rotatorWalkX.Add(true);
            rotatorWalkX.Add(0f);
        }

        public BabyMinion()
        {
            setDefault();

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
            body.children[0].createTopHalfSphere(0.0f, 0.48f, 0.0f, 0.55f);

            body.children.Add(new Asset3d());
            body.children[1].createBottomHalfSphere(0.0f, -0.50f, 0.0f, 0.55f);

            diaper = new Asset3d();
            diaper.createBottomHalfSphereElliptical(0.0f, -0.65f, 0.0f, 0.43f, 1.32f);


            //Hairs
            hairs = new Asset3d();
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());
            hairs.children.Add(new Asset3d());

            hairs.AddCoordinates(0.0f, 0.8f, 0.0f);
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
            body.children.Add(glasses);
            body.children.Add(glassesStrap);
            //body.children.Add(eye);
            body.children.Add(eyeWhite);
            body.children.Add(mouth);
            body.children.Add(tooth);
            body.children.Add(diaper);
            body.children.Add(hairs);
            body.children.Add(leftArm);
            body.children.Add(rightArm);
            body.children.Add(leftLeg);
            body.children.Add(rightLeg);
            leftArm.children.Add(leftHand);
            rightArm.children.Add(rightHand);
            eyeWhite.children.Add(eye);
            leftLeg.children.Add(leftFoot);
            rightLeg.children.Add(rightFoot);

            //Set CenterPosition
            body._centerPosition.X = 0.0f;
            body._centerPosition.Y = 0.0f;
            body._centerPosition.Z = 0.0f;

            leftArm._centerPosition = new Vector3(-0.5f, -0.2f, 0.0f);
            rightArm._centerPosition = new Vector3(0.5f, -0.2f, 0.0f);

            leftLeg._centerPosition.Y = -0.9f;
            rightLeg._centerPosition.Y = -0.9f;



        }


        public void Load()
        {
            // Body
            body.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);

            body.children[1].Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);

            //Glasses
            glasses.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", SizeX, SizeY);
            glassesStrap.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);

            //Eyes
            eyeWhite.Load(Constants.path + "white.vert", Constants.path + "white.frag", SizeX, SizeY);
            eye.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", SizeY, SizeY);
            eye.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeY, SizeY);

            //Mouth
            mouth.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            mouth.Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", SizeX, SizeY);
            GL.Uniform4(GL.GetUniformLocation(mouth._shader.Handle, "ourColor"), 1.0f, 0.31f, 0.32f, 1.0f);
            tooth.Load(Constants.path + "white.vert", Constants.path + "white.frag", SizeX, SizeY);

            //Suit

            diaper.Load(Constants.path + "white.vert", Constants.path + "white.frag", SizeX, SizeY);

            //Arms
            rightArm.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);

            leftArm.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);
            
            //Hands
            leftHand.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);

            rightHand.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);

            //Legs
            leftLeg.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);

            rightLeg.Load(Constants.path + "banana_yellow.vert", Constants.path + "banana_yellow.frag", SizeX, SizeY);

            rightFoot.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);

            leftFoot.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);

            //Hairs
            hairs.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", SizeX, SizeY);
        }

        
        public void Render(Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        {
            //Main Body & Suit
            body.Render(temp, 1, camera_view, camera_projection);
            body.children[0].Render(temp, 1, camera_view, camera_projection);
            body.children[1].Render(temp, 1, camera_view, camera_projection);
            diaper.Render(temp, 1, camera_view, camera_projection);

            //Arms & Hands
            rightArm.Render(temp, 1, camera_view, camera_projection);
            leftArm.Render(temp, 1, camera_view, camera_projection);
            leftHand.Render(temp, 0, camera_view, camera_projection);
            leftHand.children[0].Render(temp, 0, camera_view, camera_projection);
            leftHand.children[1].Render(temp, 0, camera_view, camera_projection);
            leftHand.children[2].Render(temp, 0, camera_view, camera_projection);
            rightHand.Render(temp, 0, camera_view, camera_projection);
            rightHand.children[0].Render(temp, 0, camera_view, camera_projection);
            rightHand.children[1].Render(temp, 0, camera_view, camera_projection);
            rightHand.children[2].Render(temp, 0, camera_view, camera_projection);

            //Eye & Glasses
            eyeWhite.Render(temp, 0, camera_view, camera_projection);
            eye.Render(temp, 0, camera_view, camera_projection);
            eye.children[0].Render(temp, 0, camera_view, camera_projection);
            glassesStrap.Render(temp, 1, camera_view, camera_projection);
            glasses.Render(temp, 0, camera_view, camera_projection);

            //Mouth
            mouth.Render(temp, 1, camera_view, camera_projection);
            tooth.Render(temp, 1, camera_view, camera_projection);
            tooth.children[0].Render(temp, 1, camera_view, camera_projection);

            //Legs & Foot
            leftLeg.Render(temp, 1, camera_view, camera_projection);
            rightLeg.Render(temp, 1, camera_view, camera_projection);
            leftFoot.Render(temp, 0, camera_view, camera_projection);
            rightFoot.Render(temp, 0, camera_view, camera_projection);

            //Hairs
            hairs.Render(temp, 0, camera_view, camera_projection);
            hairs.children[0].Render(temp, 0, camera_view, camera_projection);
            hairs.children[1].Render(temp, 0, camera_view, camera_projection);
            hairs.children[2].Render(temp, 0, camera_view, camera_projection);
            hairs.children[3].Render(temp, 0, camera_view, camera_projection);
            hairs.children[4].Render(temp, 0, camera_view, camera_projection);

            
        }

        public void rotateAll(string axis)
        {
            
            if (axis == "left")
            {
                body.rotate(body._centerPosition, new Vector3(0, 1, 0), -3f);
                
            }
            else if (axis == "right")
            {
                body.rotate(body._centerPosition, new Vector3(0, 1, 0), 3f);
                
            }   
        }

        public List<Object> animateEye(bool isFirstMove, bool goLeft, float currDegree)
        {
            List<Object> result = new List<Object>();
            
            if (isFirstMove)
            {
                if(currDegree >= 0 && currDegree < 30)
                {
                    eye.rotate(eyeWhite._centerPosition, new Vector3(0, 1, 0), 1f);
                    currDegree++;
                }
                else if(currDegree == 30) {
                    isFirstMove = false;
                    goLeft = false;
                }
            }
            else
            {
                if (!goLeft)
                {
                    if (currDegree != -30)
                    {
                        eye.rotate(eyeWhite._centerPosition, new Vector3(0, 1, 0), -1f);
                        currDegree--;
                    }
                    else if (currDegree == -30)
                    {
                        goLeft = true;
                    }
                }
                else
                {
                    if (currDegree != 30)
                    {
                        eye.rotate(eyeWhite._centerPosition, new Vector3(0, 1, 0), 1f);
                        currDegree++;
                    }
                    else if (currDegree == 30)
                    {
                        goLeft = false;
                    }
                }
                
            }

            result.Add(isFirstMove);
            result.Add(goLeft);
            result.Add(currDegree);
            return result;
        }
        public List<Object> animateRightArm(bool goUp,float currDegree)
        {
            List<Object> result = new List<object>();
            if (goUp)
            {
                if (currDegree != 30)
                {
                    leftArm.rotate(leftArm._centerPosition, Vector3.UnitZ, -2f);
                    currDegree++;
                }
                else if(currDegree == 30)
                {
                    goUp = false;
                }
            }
            else
            {
                if (currDegree != 0)
                {
                    leftArm.rotate(leftArm._centerPosition, Vector3.UnitZ, 2f);
                    currDegree--;
                }
                else if (currDegree == 0)
                {
                    goUp = true;
                }
            }

            result.Add(goUp);
            result.Add(currDegree);
            
            return result;
        }
        public List<Object> animateLeftArm(bool goUp, float currDegree)
        {

            List<Object> result = new List<object>();
            if (goUp)
            {
                if (currDegree != 30)
                {
                    rightArm.rotate(rightArm._centerPosition, Vector3.UnitZ, 2f);
                    currDegree++;
                }
                else if (currDegree == 30)
                {
                    goUp = false;
                }
            }
            else
            {
                if (currDegree != 0)
                {
                    rightArm.rotate(rightArm._centerPosition, Vector3.UnitZ, -2f);
                    currDegree--;
                }
                else if (currDegree == 0)
                {
                    goUp = true;
                }
            }

            result.Add(goUp);
            result.Add(currDegree);

            return result;
        }

        public List<Object> WalkZ(bool goUp, float currDegree)
        {
            List<Object> result = new List<Object>();
            if (goUp)
            {
                if (currDegree != 20)
                {
                    leftLeg.rotate(leftLeg._centerPosition, Vector3.UnitX, 2f);
                    rightLeg.rotate(rightLeg._centerPosition, Vector3.UnitX, -2f);
                    currDegree++;
                }
                else if (currDegree == 20)
                {
                    goUp = false;
                }
            }
            else
            {
                if (currDegree != -20)
                {
                    leftLeg.rotate(leftLeg._centerPosition, Vector3.UnitX, -2f);
                    rightLeg.rotate(rightLeg._centerPosition, Vector3.UnitX, 2f);
                    currDegree--;
                }
                else if (currDegree == -20)
                {
                    goUp = true;
                }
            }
            result.Add(goUp);
            result.Add(currDegree);
            return result;
        }

        public List<Object> WalkX(bool goUp, float currDegree)
        {
            List<Object> result = new List<Object>();
            if (goUp)
            {
                if (currDegree != 20)
                {
                    leftLeg.rotate(leftLeg._centerPosition, Vector3.UnitZ, 2f);
                    rightLeg.rotate(rightLeg._centerPosition, Vector3.UnitZ, -2f);
                    currDegree++;
                }
                else if (currDegree == 20)
                {
                    goUp = false;
                }
            }
            else
            {
                if (currDegree != -20)
                {
                    leftLeg.rotate(leftLeg._centerPosition, Vector3.UnitZ, -2f);
                    rightLeg.rotate(rightLeg._centerPosition, Vector3.UnitZ, 2f);
                    currDegree--;
                }
                else if (currDegree == -20)
                {
                    goUp = true;
                }
            }
            result.Add(goUp);
            result.Add(currDegree);
            return result;
        }

        public Matrix4 automaticAnimate(Matrix4 model)
        {
            if (timer == 0)
            {
                model = model * Matrix4.CreateTranslation(0.0f, 0.015f, 0.0f);
            }
            if (timer >= 0 && timer < 85)
            {
                model = model * Matrix4.CreateTranslation(0.0f, 0.0f, 0.02f);
                rotatorWalkZ = WalkZ((bool)rotatorWalkZ[0], (float)rotatorWalkZ[1]);
            }
            if (timer >= 85 && timer < 145)
            {
                rotatorLeftArm = animateLeftArm((bool)rotatorLeftArm[0], (float)rotatorLeftArm[1]);
            }
            if (timer >= 145 && timer < 205)
            {
                rotatorRightArm = animateRightArm((bool)rotatorRightArm[0], (float)rotatorRightArm[1]);
            }
            if (timer >= 205 && timer < 265)
            {
                rotatorLeftArm = animateLeftArm((bool)rotatorLeftArm[0], (float)rotatorLeftArm[1]);
                rotatorRightArm = animateRightArm((bool)rotatorRightArm[0], (float)rotatorRightArm[1]);
            }
            if (timer >= 265 && timer < 295)
            {
                rotateAll("left");
            }
            if (timer >= 295 && timer < 390)
            {
                model = model * Matrix4.CreateTranslation(-0.02f, 0.0f, 0.0f);
                rotatorWalkX = WalkX((bool)rotatorWalkX[0], (float)rotatorWalkX[1]);
            }


            timer++;
            return model;
        }

    }
}

