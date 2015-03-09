using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// The base AI class.
    /// Extensions of this are needed.
    /// Why? Because AI's have different "wants".
    /// Every Living Entity has an AI.
    /// </summary>
    public abstract class AI
    {
        private LivingEntity entity;

        public AI(LivingEntity entity) {
            this.entity = entity;
        }
        public abstract void Update(GameTime time);
    }
}
