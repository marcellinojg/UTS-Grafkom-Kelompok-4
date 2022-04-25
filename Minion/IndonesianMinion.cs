using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class IndonesianMinion
    {
        List<float> vertices_temp = new List<float>();
        Asset3dIndonesianMinion body;
        Asset3dIndonesianMinion eyeWhite;
        Asset3dIndonesianMinion eye;
        Asset3dIndonesianMinion glasses;
        Asset3dIndonesianMinion glassesStrap;
        Asset3dIndonesianMinion mouth;
        Asset3dIndonesianMinion tooth;
        Asset3dIndonesianMinion suit;
        Asset3dIndonesianMinion frontSuit;
        Asset3dIndonesianMinion backSuit;
        Asset3dIndonesianMinion rightHand;
        Asset3dIndonesianMinion leftHand;
        Asset3dIndonesianMinion rightArm;
        Asset3dIndonesianMinion leftArm;
        Asset3dIndonesianMinion leftLeg;
        Asset3dIndonesianMinion rightLeg;
        Asset3dIndonesianMinion leftFoot;
        Asset3dIndonesianMinion rightFoot;
        Asset3dIndonesianMinion stick;
        Asset3dIndonesianMinion flag;
        Asset3dIndonesianMinion headBand;
        Asset3dIndonesianMinion floor;
        Asset3dIndonesianMinion tiang;
        Asset3dIndonesianMinion belt;
        int timer = 0;

        List<Object> rotatorRightArm = new List<Object>();
        List<Object> rotatorWalkZ = new List<Object>();
        public Asset3dIndonesianMinion hairs;

        public void setDefault()
        {

            rotatorRightArm.Add(true);
            rotatorRightArm.Add(0f);


            rotatorWalkZ.Add(true);
            rotatorWalkZ.Add(0f);

            

        }

        public IndonesianMinion()
        {
            setDefault();

            //Glasses
            glasses = new Asset3dIndonesianMinion();
            glasses.createTorus(0.0f, 0.34f, 0.55f, 0.18f, 0.03f);

            glassesStrap = new Asset3dIndonesianMinion();
            glassesStrap.createCylinder(0.0f, 0.25f, 0.0f, 0.6f, 0.15f);

            //headband
            headBand = new Asset3dIndonesianMinion();
            headBand.createCylinder(0.0f, 0.68f, 0.0f, 0.58f, 0.08f);

            headBand.children.Add(new Asset3dIndonesianMinion());
            headBand.children[0].createCylinder(0.0f, 0.58f, 0.0f, 0.58f, 0.08f);

            //belt
            belt = new Asset3dIndonesianMinion();
            belt.createCylinder(0.0f, -0.4f, 0.0f, 0.6f, 0.08f);


            //eyes
            eyeWhite = new Asset3dIndonesianMinion();
            eyeWhite.createEllipsoid(0.0f, 0.34f, 0.465f, 0.2f, 0.2f, 0.2f);

            eye = new Asset3dIndonesianMinion();
            eye.createEllipsoid(0.0f, 0.34f, 0.575f, 0.1f, 0.1f, 0.1f);

            eye.children.Add(new Asset3dIndonesianMinion());
            eye.children[0].createEllipsoid(0.0f, 0.34f, 0.628f, 0.04f, 0.04f, 0.04f);

            eye.children.Add(new Asset3dIndonesianMinion());
            eye.children[1].createEllipsoid(0.0f, -0.4f, 0.628f, 0.05f, 0.05f, 0.05f);

            eye.children.Add(new Asset3dIndonesianMinion());
            eye.children[2].createEllipsoid(0.0f, -0.5f, 0.628f, 0.05f, 0.05f, 0.05f);

            //Mouth
            mouth = new Asset3dIndonesianMinion();
            mouth.AddCoordinates(-0.3f, 0.0f, 0.555f);
            mouth.AddCoordinates(-0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.1f, -0.15f, 0.566f);
            mouth.AddCoordinates(0.3f, 0.0f, 0.555f);
            mouth.Bezier3d();

            tooth = new Asset3dIndonesianMinion();
            tooth.AddCoordinates(-0.15f, 0.0f, 0.568f);
            tooth.AddCoordinates(-0.1f, -0.1f, 0.568f);
            tooth.AddCoordinates(-0.05f, -0.1f, 0.568f);
            tooth.AddCoordinates(0.0f, 0.0f, 0.568f);

            tooth.children.Add(new Asset3dIndonesianMinion());
            tooth.children[0].AddCoordinates(0.0f, 0.0f, 0.568f);
            tooth.children[0].AddCoordinates(0.05f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.1f, -0.1f, 0.568f);
            tooth.children[0].AddCoordinates(0.15f, 0.0f, 0.568f);

            tooth.Bezier3d();
            tooth.children[0].Bezier3d();


            //Main Body and Suits
            body = new Asset3dIndonesianMinion();
            body.createCylinder(0.0f, -0.5f, 0.0f, 0.55f, 1.0f);

            body.children.Add(new Asset3dIndonesianMinion());
            body.children[0].createTopHalfSphere(0.0f, 0.50f, 0.0f, 0.55f);

            body.children.Add(new Asset3dIndonesianMinion());
            body.children[1].createBottomHalfSphere(0.0f, -0.50f, 0.0f, 0.55f);

            suit = new Asset3dIndonesianMinion();
            suit.createBottomHalfSphereElliptical(0.0f, -0.663f, 0.0f, 0.4f, 1.32f);

            frontSuit = new Asset3dIndonesianMinion();
            frontSuit.createFrontEllipticalCylinder(0.0f, -0.663f, 0.18f, 0.2f, 0.4f, 2.0f);

            backSuit = new Asset3dIndonesianMinion();
            backSuit.createBackEllipticalCylinder(0.0f, -0.663f, -0.18f, 0.2f, 0.4f, 2.0f);


            //Hairs
            hairs = new Asset3dIndonesianMinion();
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());
            hairs.children.Add(new Asset3dIndonesianMinion());

            //rambut tengah
            hairs.AddCoordinates(0.0f, 1.0f, 0.0f);
            hairs.AddCoordinates(0.1f, 1.1f, 0.0f);
            hairs.AddCoordinates(-0.1f, 1.1f, 0.0f);
            hairs.AddCoordinates(0.4f, 1.5f, 0.0f);

            hairs.children[0].AddCoordinates(0.0f, 1.0f, 0.0f);
            hairs.children[0].AddCoordinates(-0.1f, 1.1f, 0.0f);
            hairs.children[0].AddCoordinates(0.1f, 1.1f, 0.0f);
            hairs.children[0].AddCoordinates(-0.4f, 1.5f, 0.0f);

            hairs.children[9].AddCoordinates(0.0f, 1.0f, -0.0f);
            hairs.children[9].AddCoordinates(0.1f, 1.1f, -0.0f);
            hairs.children[9].AddCoordinates(-0.1f, 1.1f, -0.0f);
            hairs.children[9].AddCoordinates(0.1f, 1.5f, -0.0f);

            hairs.children[10].AddCoordinates(0.0f, 1.0f, -0.0f);
            hairs.children[10].AddCoordinates(-0.1f, 1.1f, -0.0f);
            hairs.children[10].AddCoordinates(0.1f, 1.1f, -0.0f);
            hairs.children[10].AddCoordinates(-0.1f, 1.5f, -0.0f);

            //rambut depan
            hairs.children[1].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[1].AddCoordinates(0.1f, 1.1f, 0.2f);
            hairs.children[1].AddCoordinates(-0.1f, 1.1f, 0.2f);
            hairs.children[1].AddCoordinates(0.4f, 1.5f, 0.2f);

            hairs.children[2].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[2].AddCoordinates(-0.1f, 1.1f, 0.2f);
            hairs.children[2].AddCoordinates(0.1f, 1.1f, 0.2f);
            hairs.children[2].AddCoordinates(-0.4f, 1.5f, 0.2f);

            hairs.children[11].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[11].AddCoordinates(0.1f, 1.1f, 0.2f);
            hairs.children[11].AddCoordinates(-0.1f, 1.1f, 0.2f);
            hairs.children[11].AddCoordinates(0.1f, 1.5f, 0.2f);

            hairs.children[12].AddCoordinates(0.0f, 1.0f, 0.2f);
            hairs.children[12].AddCoordinates(-0.1f, 1.1f, 0.2f);
            hairs.children[12].AddCoordinates(0.1f, 1.1f, 0.2f);
            hairs.children[12].AddCoordinates(-0.1f, 1.5f, 0.2f);

            //rambut belakang
            hairs.children[3].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[3].AddCoordinates(0.1f, 1.1f, -0.2f);
            hairs.children[3].AddCoordinates(-0.1f, 1.1f, -0.2f);
            hairs.children[3].AddCoordinates(0.4f, 1.5f, -0.2f);

            hairs.children[4].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[4].AddCoordinates(-0.1f, 1.1f, -0.2f);
            hairs.children[4].AddCoordinates(0.1f, 1.1f, -0.2f);
            hairs.children[4].AddCoordinates(-0.4f, 1.5f, -0.2f);

            hairs.children[15].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[15].AddCoordinates(0.1f, 1.1f, -0.2f);
            hairs.children[15].AddCoordinates(-0.1f, 1.1f, -0.2f);
            hairs.children[15].AddCoordinates(0.1f, 1.5f, -0.2f);

            hairs.children[16].AddCoordinates(0.0f, 1.0f, -0.2f);
            hairs.children[16].AddCoordinates(-0.1f, 1.1f, -0.2f);
            hairs.children[16].AddCoordinates(0.1f, 1.1f, -0.2f);
            hairs.children[16].AddCoordinates(-0.1f, 1.5f, -0.2f);

            //rambut sela depan
            hairs.children[5].AddCoordinates(0.0f, 1.0f, 0.1f);
            hairs.children[5].AddCoordinates(0.1f, 1.1f, 0.1f);
            hairs.children[5].AddCoordinates(-0.1f, 1.1f, 0.1f);
            hairs.children[5].AddCoordinates(0.4f, 1.5f, 0.1f);

            hairs.children[6].AddCoordinates(0.0f, 1.0f, 0.1f);
            hairs.children[6].AddCoordinates(-0.1f, 1.1f, 0.1f);
            hairs.children[6].AddCoordinates(0.1f, 1.1f, 0.1f);
            hairs.children[6].AddCoordinates(-0.4f, 1.5f, 0.1f);

            hairs.children[17].AddCoordinates(0.0f, 1.0f, 0.1f);
            hairs.children[17].AddCoordinates(0.1f, 1.1f, 0.1f);
            hairs.children[17].AddCoordinates(-0.1f, 1.1f, 0.1f);
            hairs.children[17].AddCoordinates(0.1f, 1.5f, 0.1f);

            hairs.children[18].AddCoordinates(0.0f, 1.0f, 0.1f);
            hairs.children[18].AddCoordinates(-0.1f, 1.1f, 0.1f);
            hairs.children[18].AddCoordinates(0.1f, 1.1f, 0.1f);
            hairs.children[18].AddCoordinates(-0.1f, 1.5f, 0.1f);

            //rambut sela belakang
            hairs.children[7].AddCoordinates(0.0f, 1.0f, -0.1f);
            hairs.children[7].AddCoordinates(0.1f, 1.1f, -0.1f);
            hairs.children[7].AddCoordinates(-0.1f, 1.1f, -0.1f);
            hairs.children[7].AddCoordinates(0.4f, 1.5f, -0.1f);

            hairs.children[8].AddCoordinates(0.0f, 1.0f, -0.1f);
            hairs.children[8].AddCoordinates(-0.1f, 1.1f, -0.1f);
            hairs.children[8].AddCoordinates(0.1f, 1.1f, -0.1f);
            hairs.children[8].AddCoordinates(-0.4f, 1.5f, -0.1f);

            hairs.children[13].AddCoordinates(0.0f, 1.0f, -0.1f);
            hairs.children[13].AddCoordinates(0.1f, 1.1f, -0.1f);
            hairs.children[13].AddCoordinates(-0.1f, 1.1f, -0.1f);
            hairs.children[13].AddCoordinates(0.1f, 1.5f, -0.1f);

            hairs.children[14].AddCoordinates(0.0f, 1.0f, -0.1f);
            hairs.children[14].AddCoordinates(-0.1f, 1.1f, -0.1f);
            hairs.children[14].AddCoordinates(0.1f, 1.1f, -0.1f);
            hairs.children[14].AddCoordinates(-0.1f, 1.5f, -0.1f);

            hairs.Bezier3d();
            hairs.children[0].Bezier3d();
            hairs.children[1].Bezier3d();
            hairs.children[2].Bezier3d();
            hairs.children[3].Bezier3d();
            hairs.children[4].Bezier3d();
            hairs.children[5].Bezier3d();
            hairs.children[6].Bezier3d();
            hairs.children[7].Bezier3d();
            hairs.children[8].Bezier3d();
            hairs.children[9].Bezier3d();
            hairs.children[10].Bezier3d();
            hairs.children[11].Bezier3d();
            hairs.children[12].Bezier3d();
            hairs.children[13].Bezier3d();
            hairs.children[14].Bezier3d();
            hairs.children[15].Bezier3d();
            hairs.children[16].Bezier3d();
            hairs.children[17].Bezier3d();
            hairs.children[18].Bezier3d();

            //Arms
            rightArm = new Asset3dIndonesianMinion();
            rightArm.createHorizontalCylinder(0.5f, -0.2f, 0.0f, 0.03f, 0.4f);

            leftArm = new Asset3dIndonesianMinion();
            leftArm.createHorizontalCylinder(-0.9f, -0.2f, 0.0f, 0.03f, 0.4f);

            //Hands
            leftHand = new Asset3dIndonesianMinion();
            leftHand.createEllipsoid(-0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            rightHand = new Asset3dIndonesianMinion();
            rightHand.createEllipsoid(0.9f, -0.2f, 0.0f, 0.04f, 0.05f, 0.05f);

            //Legs
            leftLeg = new Asset3dIndonesianMinion();
            leftLeg.createCylinder(-0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            rightLeg = new Asset3dIndonesianMinion();
            rightLeg.createCylinder(0.2f, -1.2f, 0.0f, 0.08f, 0.3f);

            //Foots
            leftFoot = new Asset3dIndonesianMinion();
            leftFoot.createEllipsoid(-0.2f, -1.2f, 0.05f, 0.08f, 0.08f, 0.12f);

            rightFoot = new Asset3dIndonesianMinion();
            rightFoot.createEllipsoid(0.2f, -1.2f, 0.05f, 0.08f, 0.08f, 0.12f);

            //Fingers left
            leftHand.children.Add(new Asset3dIndonesianMinion());
            leftHand.children[0].createEllipsoid(-0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3dIndonesianMinion());
            leftHand.children[1].createEllipsoid(-0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            leftHand.children.Add(new Asset3dIndonesianMinion());
            leftHand.children[2].createEllipsoid(-0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);

            //Fingers right
            rightHand.children.Add(new Asset3dIndonesianMinion());
            rightHand.children[0].createEllipsoid(0.95f, -0.20f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3dIndonesianMinion());
            rightHand.children[1].createEllipsoid(0.95f, -0.23f, 0.0f, 0.04f, 0.025f, 0.025f);

            rightHand.children.Add(new Asset3dIndonesianMinion());
            rightHand.children[2].createEllipsoid(0.95f, -0.17f, 0.0f, 0.04f, 0.025f, 0.025f);

            //Inheritances
            leftArm.children.Add(leftHand);
            rightArm.children.Add(rightHand);

            eyeWhite.children.Add(eye);
            

            //stick
            stick = new Asset3dIndonesianMinion();
            stick.createCylinder(-0.92f, -0.4f, 0.0f, 0.05f, 1.0f);

            stick.children.Add(new Asset3dIndonesianMinion());
            stick.children[0].createEllipsoid(-0.92f, 0.64f, 0.0f, 0.06f, 0.06f, 0.06f);

            //flag
            flag = new Asset3dIndonesianMinion();
            flag.createHorizontalCylinder(-1.46f, 0.55f, 0.0f, 0.05f, 0.5f);

            flag.children.Add(new Asset3dIndonesianMinion());
            flag.children[0].createHorizontalCylinder(-1.46f, 0.45f, 0.0f, 0.05f, 0.5f);

            //floor
            floor = new Asset3dIndonesianMinion();
            floor.createBoxVertices(0.0f, -1.4f, 0.05f, 6.5f, 8.0f, 0.3f);

            floor.children.Add(new Asset3dIndonesianMinion());
            floor.children[0].createBoxVertices(-2.0f, -1.39f, 0.05f, 0.4f, 8.0f, 0.3f);

            floor.children.Add(new Asset3dIndonesianMinion());
            floor.children[1].createBoxVertices(2.0f, -1.39f, 0.05f, 0.4f, 8.0f, 0.3f);

            floor.children.Add(new Asset3dIndonesianMinion());
            floor.children[2].createBoxVertices(0.0f, -1.39f, 2.05f, 0.4f, 2.0f, 0.3f);

            floor.children.Add(new Asset3dIndonesianMinion());
            floor.children[3].createBoxVertices(0.0f, -1.39f, -1.95f, 0.4f, -2.0f, 0.3f);


            //tiangAbuAbu
            tiang = new Asset3dIndonesianMinion();
            tiang.createCylinder(2.0f, -1.28f, 0.05f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[0].createCylinder(-2.0f, -1.28f, 0.05f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[1].createCylinder(2.0f, -1.28f, -0.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[2].createCylinder(-2.0f, -1.28f, -0.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[3].createCylinder(2.0f, -1.28f, -1.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[4].createCylinder(-2.0f, -1.28f, -1.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[5].createCylinder(2.0f, -1.28f, -2.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[6].createCylinder(-2.0f, -1.28f, -2.95f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[7].createCylinder(2.0f, -1.28f, 1.05f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[8].createCylinder(-2.0f, -1.28f, 1.05f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[9].createCylinder(2.0f, -1.28f, 2.05f, 0.05f, 1.0f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[10].createCylinder(-2.0f, -1.28f, 2.05f, 0.05f, 1.0f);

            //ujungTiangMerah
            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[11].createEllipsoid(2.0f, -0.24f, 0.05f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[12].createEllipsoid(-2.0f, -0.24f, 0.05f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[13].createEllipsoid(2.0f, -0.24f, -0.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[14].createEllipsoid(-2.0f, -0.24f, -0.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[15].createEllipsoid(2.0f, -0.24f, -1.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[16].createEllipsoid(-2.0f, -0.24f, -1.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[17].createEllipsoid(2.0f, -0.24f, -2.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[18].createEllipsoid(-2.0f, -0.24f, -2.95f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[19].createEllipsoid(2.0f, -0.24f, 1.05f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[20].createEllipsoid(-2.0f, -0.24f, 1.05f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[21].createEllipsoid(2.0f, -0.24f, 2.05f, 0.06f, 0.06f, 0.06f);

            tiang.children.Add(new Asset3dIndonesianMinion());
            tiang.children[22].createEllipsoid(-2.0f, -0.24f, 2.05f, 0.06f, 0.06f, 0.06f);

            leftArm.children.Add(flag);
            leftArm.children.Add(stick);
            leftLeg.children.Add(leftFoot);
            rightLeg.children.Add(rightFoot);

        }


        public void Load()
        {
            //Glasses
            glasses.Load(Constants.path + "spanish_gray.vert", Constants.path + "spanish_gray.frag", 800, 800);
            glassesStrap.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            headBand.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            headBand.children[0].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);

            belt.Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);

            //Eyes
            eyeWhite.Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            eye.Load(Constants.path + "cafe_au_lait.vert", Constants.path + "cafe_au_lait.frag", 800, 800);
            eye.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);
            eye.children[1].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            eye.children[2].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);

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
            hairs.children[0].Load(Constants.path + "raisin_black.vert", Constants.path + "raisin_black.frag", 800, 800);


            //stick
            stick.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            stick.children[0].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);

            //flagAtasBawah
            flag.Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            flag.children[0].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);

            //floor
            floor.Load(Constants.path + "floor.vert", Constants.path + "floor.frag", 800, 800);
            floor.children[0].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            floor.children[1].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            floor.children[2].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);
            floor.children[3].Load(Constants.path + "white.vert", Constants.path + "white.frag", 800, 800);

            //tiang
            tiang.Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[0].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[1].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[2].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[3].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[4].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[5].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[6].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[7].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[8].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[9].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[10].Load(Constants.path + "gray.vert", Constants.path + "gray.frag", 800, 800);
            tiang.children[11].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[12].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[13].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[14].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[15].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[16].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[17].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[18].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[19].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[20].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[21].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);
            tiang.children[22].Load(Constants.path + "flagRed.vert", Constants.path + "flagRed.frag", 800, 800);


        }



        public void Render(Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        {
            //Main Body & Suit

            body.Render(temp, 1, camera_view, camera_projection);
            suit.Render(temp, 0, camera_view, camera_projection);
            frontSuit.Render(temp, 1, camera_view, camera_projection);
            backSuit.Render(temp, 1, camera_view, camera_projection);

            //headBand
            headBand.Render(temp, 1, camera_view, camera_projection);

            //belt
            belt.Render(temp, 1, camera_view, camera_projection);

            //Arms & Hands
            rightArm.Render(temp, 1, camera_view, camera_projection);
            leftArm.Render(temp, 1, camera_view, camera_projection);
            leftHand.Render(temp, 0, camera_view, camera_projection);
            rightHand.Render(temp, 0, camera_view, camera_projection);

            //Eye & Glasses
            eyeWhite.Render(temp, 0, camera_view, camera_projection);
            eye.Render(temp, 0, camera_view, camera_projection);
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

            //stick
            stick.Render(temp, 0, camera_view, camera_projection);

            //flag
            flag.Render(temp, 0, camera_view, camera_projection);

            ////floor
            //floor.Render(temp, 2, camera_view, camera_projection);

            ////tiang
            //tiang.Render(temp, 0, camera_view, camera_projection);



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

        public List<Object> animateRightArm(bool goUp, float currDegree)
        {
            List<Object> result = new List<object>();
            if (goUp)
            {
                if (currDegree != 30)
                {
                    leftArm.rotate(leftArm._centerPosition, Vector3.UnitY, 2f);
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
                    leftArm.rotate(leftArm._centerPosition, Vector3.UnitY, -2f);
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

        public Matrix4 automaticAnimate(Matrix4 model)
        {
            if(timer == 0)
            {
                leftArm._centerPosition = new Vector3(-0.5f, -0.2f, 0.0f);
                leftLeg._centerPosition.Y = -0.9f;
                rightLeg._centerPosition.Y = -0.9f;
            }
            if(timer >= 0 && timer < 80)
            {
                model = model * Matrix4.CreateTranslation(0.0f, 0.0f, 0.02f);
                rotatorWalkZ = WalkZ((bool)rotatorWalkZ[0], (float)rotatorWalkZ[1]);
                rotatorRightArm = animateRightArm((bool)rotatorRightArm[0], (float)rotatorRightArm[1]);
            }
            if (timer >= 80 && timer < 160)
            {
                rotatorRightArm = animateRightArm((bool)rotatorRightArm[0], (float)rotatorRightArm[1]);
                model = model * Matrix4.CreateTranslation(0.0f, 0.0f, -0.02f);
                rotatorWalkZ = WalkZ((bool)rotatorWalkZ[0], (float)rotatorWalkZ[1]);
            }
            if(timer == 160)
            {
                timer = 0;
            }

            timer++;
            return model;
        }


    }
}
