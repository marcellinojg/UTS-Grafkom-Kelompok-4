using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
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
        float degree = 0;
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

            minion.Load();

            
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 temp = Matrix4.Identity;
            temp = temp * Matrix4.CreateRotationX(degree);
            temp = temp * Matrix4.CreateRotationY(degree);
            degree += MathHelper.DegreesToRadians(1.0f);
            minion.Render(temp);

            

            SwapBuffers();
        }
    }
    
}
