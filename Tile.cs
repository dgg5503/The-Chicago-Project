using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject
{
    /// <summary>
    /// The different types of tiles.
    /// 
    /// Each tiletype is also a filename!
    /// </summary>
    enum TileType
    {
        RoadTar,
        RoadLine,
        SideWalkBrick,
        // More to come :D!
    }

    /*
     * Tile Notes:
     * - All tiles will be 64x64 in size.
     * - X and Y are defined by the upper left side corner
     * - By default, isWalkable is set to true.
     * 
     * 
     * TO-DO:
     * - Custom tile colors (1)
     * - Base constructor with no location defined?
     * 
     */

    class Tile
    {
        // Fields

        // The tile's texture
        private Texture2D texture;

        // Tile's location and size
        private Rectangle sizeLoc;

        // Bool for whether or not it can be walked on (ASHWIN'S HOLY GRAIL)
        private bool isWalkable;

        // The TileType for easy identification.
        private TileType tileType;

        // Const width/height
        private const int SIDE_LENGTH = 64;

        // Properties
        // These are self explanatory, I dont think I have to comment what each one of these do...
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        /// <summary>
        /// X pixel based location.
        /// </summary>
        public int X { get { return sizeLoc.X; } }
        /// <summary>
        /// Y pixel based location.
        /// </summary>
        public int Y { get { return sizeLoc.Y; } }

        /// <summary>
        /// Location relative to the side_length. i.e. X / SIDE_LENGTH
        /// </summary>
        public int TileX { get { return sizeLoc.X / SIDE_LENGTH; } }

        /// <summary>
        /// Location relative to the side_length. i.e. Y / SIDE_LENGTH
        /// </summary>
        public int TileY { get { return sizeLoc.Y / SIDE_LENGTH;  } }

        public Rectangle SizeLoc { get { return sizeLoc; } }

        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public TileType TileType { get { return tileType; } } // This cannot be changed.


        /// <summary>
        /// Creates a tile.
        /// </summary>
        /// <param name="x">X location of tile in pixels.</param>
        /// <param name="y">Y location of tile in pixels.</param>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="tileType">The type of tile you wish to create.</param>
        public Tile(int x, int y, Texture2D texture, TileType tileType)
        {
            isWalkable = true;
            this.sizeLoc = new Rectangle(x, y, SIDE_LENGTH, SIDE_LENGTH);
            this.texture = texture;
            this.tileType = tileType;
        }

        /// <summary>
        /// Basic draw method to draw the tile.
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1.</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, sizeLoc, Color.White); //(1)
        }

    }
}
