using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Minion
{
    class Window : GameWindow
    {
        BabyMinion babyMinion;
        Environment env;
        Camera camera;
        bool firstMove = true;
        Vector2 lastPos;
        bool cameraMode = true;
        Matrix4 _minionModel = Matrix4.Identity;
        Matrix4 _environmentModel = Matrix4.Identity;
        List<Object> rotatorEye = new List<Object>();
        List<Object> rotatorRightArm = new List<Object>();
        List<Object> rotatorLeftArm = new List<Object>();
        List<Object> rotatorWalkZ = new List<Object>();
        List<Object> rotatorWalkX = new List<Object>();

        static class Constants
        {
            public const string path = "../../../Shaders/";
        }

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

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

            babyMinion = new BabyMinion();
            env = new Environment();

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.52f, 0.8f, 0.92f, 1.0f);

            setDefault();
           
            camera = new Camera(new Vector3(0, 0.3f, 3.0f), Size.X / Size.Y);
            CursorGrabbed = true;

            env.Load();
            babyMinion.Load();

            _minionModel = _minionModel * Matrix4.CreateScale(0.7f);
            _minionModel = _minionModel * Matrix4.CreateScale(0.7f);
            _minionModel = _minionModel * Matrix4.CreateScale(0.7f);
            _minionModel = _minionModel * Matrix4.CreateScale(0.7f);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            camera.AspectRatio = Size.X / (float)Size.Y;

            GL.Viewport(0, 0, Size.X, Size.Y);

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            float cameraSpeed = 0.9f;
            var mouse = MouseState;
            var sensitivity = 0.1f;

            //Mode camera atau object
            if (KeyboardState.IsKeyPressed(Keys.F5))
            {
                if (cameraMode)
                {
                    cameraMode = false;
                }
                else
                {
                    cameraMode = true;
                }
            }

            //Camera Mode
            if (cameraMode)
            {
                if (firstMove)
                {
                    lastPos = new Vector2(mouse.X, mouse.Y);
                    firstMove = false;
                }
                else
                {
                    var deltaX = mouse.X - lastPos.X;
                    var deltaY = mouse.Y - lastPos.Y;

                    lastPos = new Vector2(mouse.X, mouse.Y);
                    camera.Yaw += deltaX * sensitivity;
                    camera.Pitch -= deltaY * sensitivity;
                }

                //Camera maju
                if (KeyboardState.IsKeyDown(Keys.W))
                {
                    camera.Position += camera.Front * cameraSpeed * (float)args.Time;
                }
                //Camera mundur
                if (KeyboardState.IsKeyDown(Keys.S))
                {
                    camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
                }
                //Camera Kanan
                if (KeyboardState.IsKeyDown(Keys.D))
                {
                    camera.Position += camera.Right * cameraSpeed * (float)args.Time;
                }
                //Camera Kiri
                if (KeyboardState.IsKeyDown(Keys.A))
                {
                    camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
                }
                //Camera naik
                if (KeyboardState.IsKeyDown(Keys.Space))
                {
                    camera.Position += camera.Up * cameraSpeed * (float)args.Time;
                }
                //Camera turun
                if (KeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    camera.Position -= camera.Up * cameraSpeed * (float)args.Time;
                }
            }
            //Object Mode
            else
            {
                //Maju
                if (KeyboardState.IsKeyDown(Keys.W))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(0.0f, 0.0f, 0.02f);
                    rotatorWalkZ = babyMinion.WalkZ((bool)rotatorWalkZ[0],(float)rotatorWalkZ[1]);
                }
                //Mundur
                if (KeyboardState.IsKeyDown(Keys.S))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(0.0f, 0.0f, -0.02f);
                    rotatorWalkZ = babyMinion.WalkZ((bool)rotatorWalkZ[0], (float)rotatorWalkZ[1]);
                }
                //Kanan
                if (KeyboardState.IsKeyDown(Keys.D))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(0.02f, 0.0f, 0.0f);
                    rotatorWalkX = babyMinion.WalkX((bool)rotatorWalkX[0], (float)rotatorWalkX[1]);
                }
                //Kiri
                if (KeyboardState.IsKeyDown(Keys.A))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(-0.02f, 0.0f, 0.0f);
                    rotatorWalkX = babyMinion.WalkX((bool)rotatorWalkX[0], (float)rotatorWalkX[1]);
                }
                //Naik
                if (KeyboardState.IsKeyDown(Keys.Space))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(0.0f, 0.015f, 0.0f);
                }
                //Turun
                if (KeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    _minionModel = _minionModel * Matrix4.CreateTranslation(0.0f, -0.015f, 0.0f);
                }
                //Rotate kiri
                if (KeyboardState.IsKeyDown(Keys.Left))
                {
                    babyMinion.rotateAll("left");
                }
                //Rotate kanan
                if (KeyboardState.IsKeyDown(Keys.Right))
                {
                    babyMinion.rotateAll("right");
                }

                //Animasi tangan
                if (MouseState.IsButtonDown(MouseButton.Left))
                {
                    rotatorLeftArm = babyMinion.animateLeftArm((bool)rotatorLeftArm[0], (float)rotatorLeftArm[1]);
                }
                if (MouseState.IsButtonDown(MouseButton.Right))
                {
                    rotatorRightArm = babyMinion.animateRightArm((bool)rotatorRightArm[0], (float)rotatorRightArm[1]);
                }
            }
            

            if (KeyboardState.IsKeyPressed(Keys.F3))
            {
                _minionModel = _minionModel * Matrix4.CreateScale(0.9f);
                Console.WriteLine("Scale down");
            }
            if (KeyboardState.IsKeyPressed(Keys.F4))
            {
                _minionModel = _minionModel * Matrix4.CreateScale(1.1f);
                Console.WriteLine("Scale up");
            }

            rotatorEye = babyMinion.animateEye((bool)rotatorEye[0], (bool)rotatorEye[1], (float)rotatorEye[2]);
            

            babyMinion.Render(_minionModel,camera.GetViewMatrix(),camera.GetProjectionMatrix());
            env.Render(_environmentModel, camera.GetViewMatrix(), camera.GetProjectionMatrix());
            //_minionModel = babyMinion.automaticAnimate(_minionModel);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }

     


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.Fov = camera.Fov - e.OffsetY;
        }

        
    }
    
}
