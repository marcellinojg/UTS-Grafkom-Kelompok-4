using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minion
{
    internal class MinionHead
    {
        List<Asset3d> hair;
        public MinionHead()
        {
            hair = new List<Asset3d>();
            hair.Add(new Asset3d());

            hair[0].addCoordinatesToVertices(0.0f, 0.0f, 0.0f);
            hair[0].addCoordinatesToVertices(0.3f, 0.5f, 0.0f);
            hair[0].createCurveBezier();


        }

        public void Load()
        {
            for (int i = 0; i < hair.Count; i++)
            {
                hair[i].Load(Constants.path + "uniform.vert", Constants.path + "uniform.frag", 800, 800);
                GL.Uniform4(GL.GetUniformLocation(hair[i]._shader.Handle, "ourColor"), 0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        public void Render(Matrix4 temp)
        {
            for (int i = 0; i < hair.Count; i++)
            {
                hair[i].Render(temp);
            }
        }
    }
}
