//Josiah DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.Item
{
    public class Item
    {
        public string name;
        public Sprite previewSprite;
        public Sprite equipedSprite;

        // all items will now loade with default null texture to preven crashing.
        public Item()
        {
            previewSprite = Sprites.spritesDictionary["NULL"];
            equipedSprite = Sprites.spritesDictionary["NULL"];
        }
    }
}
