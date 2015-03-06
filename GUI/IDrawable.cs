using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.GUI
{
    interface IDrawable
    {
        /// <summary>
        /// All drawable objects must have a texture.
        /// Allows for easy polymorphism, we could possibly have one giant list to set textures!
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// All drawable objects must have atleast this basic draw method.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        //void Draw(SpriteBatch sb, int x, int y);
    }
}
