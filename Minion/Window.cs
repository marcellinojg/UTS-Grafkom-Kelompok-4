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
        Minion minion;
        float degreeVertical = 0;
        float degreeHorizontal = 0;
        Camera camera;
        bool firstMove = true;
        Vector2 lastPos;
        Vector3 objectPos = new Vector3(0.0f, 0.0f, 0.0f);
        float rotationSpeed = 0.5f;
        Matrix4 temp = Matrix4.Identity;
        static class Constants
        {
            public const string path = "../../../Shaders/";
        }

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.0f, 1.0f, 1.0f, 1.0f);

            minion = new Minion();
            camera = new Camera(new Vector3(0, 0, 1), Size.X / Size.Y);
            CursorGrabbed = true;

            minion.Load();

            



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
            Matrix4 temp = Matrix4.Identity;

            float cameraSpeed = 0.5f;


            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= objectPos;
                camera.Position = Vector3.Transform(
                    camera.Position,
                    generateArbRotationMatrix(axis, objectPos, rotationSpeed)
                    .ExtractRotation());
                camera.Position += objectPos;
                camera._front = -Vector3.Normalize(camera.Position
                    - objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= objectPos;
                camera.Yaw -= rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, objectPos, -rotationSpeed)
                    .ExtractRotation());
                camera.Position += objectPos;

                camera._front = -Vector3.Normalize(camera.Position - objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= objectPos;
                camera.Pitch -= rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, objectPos, rotationSpeed).ExtractRotation());
                camera.Position += objectPos;
                camera._front = -Vector3.Normalize(camera.Position - objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= objectPos;
                camera.Pitch += rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, objectPos, -rotationSpeed).ExtractRotation());
                camera.Position += objectPos;
                camera._front = -Vector3.Normalize(camera.Position - objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)args.Time;
            }


            if (KeyboardState.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
            }

            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)args.Time;
            }

            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)args.Time;
            }

            var mouse = MouseState;

            var sensitivity = 0.2f;

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




            minion.Render(temp,camera.GetViewMatrix(),camera.GetProjectionMatrix());

            

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
