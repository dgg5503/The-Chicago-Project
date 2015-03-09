using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.Math;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// Keeps his distance.
    /// Will attempt to shoot you / grenade you.
    /// Stays within 5-7 tiles, and moves away if you get close.
    /// </summary>
    class MidAI : AI
    {
        public MidAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time) {

        }
    }
}
