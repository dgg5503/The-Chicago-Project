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
    /// Death from afar.
    /// Will shoot you from 12+ tiles, preferrably.
    /// Will only run if you're within 8 tiles.
    /// </summary>
    class HighAI : AI
    {
        public HighAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time) {
            
        }
    }
}
