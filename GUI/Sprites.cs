using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner
    static class Sprites
    {
        // Dictionary of tiles and their file name.
        public readonly static Dictionary<string, Sprite> spritesDictionary = new Dictionary<string, Sprite>
        {
            {"mugger", new Sprite(32, 32, 0, "mugger.png")}
        };
    }
}
