//Josiah S DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.Entity
{
    /// <summary>
    /// The base entity class.
    /// </summary>
    public abstract class Entity
    {
        public Rectangle location;
        
        public Direction direction;

        public Sprite sprite;

        /// <summary>
        /// The constructor for the base entity.
        /// </summary>
        /// <param name="fileName">The texture filename for the entity.</param>
        /// <param name="location">The height, width, and X and Y location.</param>
        public Entity(Rectangle location, string fileName)
        {
            this.location = location;

            /*
             * We would need a custom file format in order to take in a sprite
             * and properly parse it for animation and what not.
             * 
             * AS OF NOW, all entities have a "simple" sprite which is
             * basically a static image (Can be rotated within the draw params.) (IWGTI)
             * 
             * 
             * TO RESIZE A SPRITE, EITHER DO SPRITE.RESIZE OR INSTANTIATE VIA
             * THE HEIGHT AND WIDTH FROM LOCATION RECTANGLE?????
             */
            sprite = new Sprite(location.Height, location.Width, 0, fileName);
        }

        /// <summary>
        /// Updates the Game Time and the Entity Manager of the Entity
        /// </summary>
        /// <param name="time">The game Time</param>
        /// <param name="manager">The EntityManager that links to the player</param>
        public virtual void Update(GameTime time, EntityManager manager) { }

        /// <summary>
        /// Moves the Entity
        /// </summary>
        public virtual void Move()
        {
            throw new NotImplementedException();
        }
    }
}
