using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Josiah Divizia
namespace TheChicagoProject.GUI
{
    public class Door : Tile
    {
        private string world;
        private Microsoft.Xna.Framework.Vector2 destination;

        /// <summary>
        /// Gets the file name of the world the door links to
        /// </summary>
        public string World { get { return world; } }

        /// <summary>
        /// Gets the location the player will end up in in the other world
        /// </summary>
        public Microsoft.Xna.Framework.Vector2 Destination { get { return destination; } }

        /// <summary>
        /// Creates a new door object
        /// </summary>
        /// <param name="world">The file name of the door that the door links to</param>
        public Door(string world, int x, int y) : base(true, "door.png")
        {
            this.world = world;
            destination = new Microsoft.Xna.Framework.Vector2(x, y);
            this.Texture = Game1.Instance.Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("./Tiles/door.png");
        }
    }
}
