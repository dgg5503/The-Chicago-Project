using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.Entity;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner

    /*
     * Tile Notes:
     * - All tiles will be 64x64 in size.
     * - By default, isWalkable is set to true.
     * 
     */

    public class Tile
    {
        // Fields

        //--- COLLISION STUFF ---

        // Every tile holds entities.
        private List<Entity.Entity> entities;

        //--- COLLISION STUFF ---

        // The tile's texture
        private Texture2D texture;

        // Bool for whether or not it can be walked on (ASHWIN'S HOLY GRAIL)
        private bool isWalkable;

        // File name with extension (blah.png)
        private string fileName;

        // Const width/height
        public const int SIDE_LENGTH = 64;

        // Directory of tiles
        public const string Directory = "./Content/Tiles/";

        // Properties
        // These are self explanatory, I dont think I have to comment what each one of these do...
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public string FileName { get { return fileName; } } // needed if we're doing a dictionary??? (?)


        /// <summary>
        /// Creates a tile (texture not set yet!)
        /// </summary>
        /// <param name="isWalkable">Whether or not the tile can be walked on.</param>
        /// <param name="fileName">The tiles file name with extension.</param>
        public Tile(bool isWalkable, string fileName)
        {
            this.isWalkable = isWalkable;
            this.fileName = fileName;
        }

        /// <summary>
        /// Basic draw method to draw the tile.
        /// NOTE: All tiles are 64x64, no need to specify a w and h
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1.</param>
        /// <param name="x">X location in pixels.</param>
        /// <param name="y">Y location in pixels.</param>
        public void Draw(SpriteBatch sb, int x, int y)
        {
            sb.Draw(texture, new Rectangle(x, y, SIDE_LENGTH, SIDE_LENGTH), Color.White);
        }

        /// <summary>
        /// Basic draw method to draw the tile (with a custom color).
        /// NOTE: All tiles are 64x64, no need to specify a w and h
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1.</param>
        /// <param name="x">X location in pixels.</param>
        /// <param name="y">Y location in pixels.</param>
        /// <param name="c">A color to overlay the image.</param>
        public void Draw(SpriteBatch sb, int x, int y, Color c)
        {
            sb.Draw(texture, new Rectangle(x, y, SIDE_LENGTH, SIDE_LENGTH), c);
        }

    }
}
