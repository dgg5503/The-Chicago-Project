using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner
    static class Sprites
    {
        // Dictionary of sprites and their file name.
        /*
         * NOTES:
         * - Add all sprites in the entire game to this dicitionary so they can be grabbed at any time during the game!
         */

        public readonly static Dictionary<string, Sprite> spritesDictionary = new Dictionary<string, Sprite>
        {
            {"mugger", new Sprite(32, 32, 0, "mugger.png")},
            {"player", new Sprite(32, 32, 0, "player.png")},
            {"gatling_gun_preview", new Sprite(64, 64, 0, "gatling_gun_small.png")},
            {"uzi_gun_preview", new Sprite(64, 64, 0, "uzi_gun_small.png")},
            {"basic_gun_preview", new Sprite(64, 64, 0, "basic_gun_small.png")},
            {"NULL", new Sprite(64, 64, 0, "NULL.png")}
        };
    }
}
