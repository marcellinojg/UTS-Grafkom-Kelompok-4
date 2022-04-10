using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    
    internal class MinionBody
    {
        List<Asset3d> objects;
        public MinionBody()
        {
            
            objects = new List<Asset3d>();

            objects.Add(new Asset3d());
            objects[0].createCylinder(0.0f, -0.5f, 0.0f, 0.45f, 1.0f);

            objects.Add(new Asset3d());
            objects[1].createTopHalfSphere(0.0f, 0.51f, 0.0f, 0.45f);

            objects.Add(new Asset3d());
            objects[2].createBottomHalfSphere(0.0f, -0.51f, 0.0f, 0.45f);
           

        }


        public void Load()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
                GL.Uniform4(GL.GetUniformLocation(objects[i]._shader.Handle, "ourColor"), 1.0f, 1.0f, 0.0f, 1.0f);
            }
        }
        public void Render(Matrix4 temp)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Render(temp);
            }
        }


    }
}
