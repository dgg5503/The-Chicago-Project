using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

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
            {"mugger", new Sprite(32, 32, 0, Color.White, "mugger.png")},
            {"player", new Sprite(32, 32, 0, Color.White, "player.png")},
            {"gatling_gun_preview", new Sprite(64, 64, 0, Color.White, "gatling_gun_preview.png")},
            {"uzi_gun_preview", new Sprite(64, 64, 0, Color.White, "uzi_gun_preview.png")},
            {"basic_gun_preview", new Sprite(64, 64, 0, Color.White, "basic_gun_preview.png")},
            {"knife_preview", new Sprite(64, 64, 0, Color.White, "knife_preview.png")},
            {"NULL", new Sprite(64, 64, 0, Color.White, "NULL.png")}
        };
    }
}
