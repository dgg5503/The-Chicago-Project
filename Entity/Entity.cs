using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    /// <summary>
    /// The base entity class.
    /// </summary>
    public abstract class Entity
    {
        public Rectangle location;

        public virtual void Update(GameTime time, EntityManager manager) { }

        public virtual void Move()
        {

        }
    }
}
