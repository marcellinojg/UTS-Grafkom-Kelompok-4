using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Minion
{
    class WindowSettings
    {
        static void Main(string[] args)
        {
            var native = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(800, 800),
                Title = "Minions"
            };

            using (var window = new Window(GameWindowSettings.Default, native))
            {
                window.Run();
            };
        }
    }
}
