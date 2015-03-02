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

        // Bool for whether or not it can be walked on (ASHWIN'S HOLY GRAIL)
        private bool isWalkable;

        // The TileType for easy identification.
        private TileType tileType;

        // Const width/height
        //private const int SIDE_LENGTH = 64;

        // Properties
        // These are self explanatory, I dont think I have to comment what each one of these do...
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public TileType TileType { get { return tileType; } } // This cannot be changed.


        /// <summary>
        /// Creates a tile.
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="tileType">The type of tile you wish to create.</param>
        /// <param name="isWalkable">Whether or not the tile can be walked on.</param>
        public Tile(Texture2D texture, TileType tileType, bool isWalkable)
        {
            this.isWalkable = isWalkable;
            this.texture = texture;
            this.tileType = tileType;
        }

        /// <summary>
        /// Basic draw method to draw the tile.
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1.</param>
        public void Draw(SpriteBatch sb, Rectangle sizeLoc)
        {
            sb.Draw(texture, sizeLoc, Color.White); //(1)
        }

    }
}
