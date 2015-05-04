using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.GUI
{
    public class Door : Tile
    {
        private string world;

        /// <summary>
        /// Gets the file name of the world the door links to
        /// </summary>
        public string World { get { return world; } }

        /// <summary>
        /// Creates a new door object
        /// </summary>
        /// <param name="world">The file name of the door that the door links to</param>
        public Door(string world) : base(true, "door.png")
        {
            this.world = world;
        }
    }
}
