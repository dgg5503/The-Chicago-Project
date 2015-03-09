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
    /// Gets up close and personal.
    /// Will knife (?) / shoot you from ~1-3 tiles.
    /// Does not care if you get close, but will chase you.
    /// </summary>
    class LowAI : AI
    {
        public LowAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time) {

        }
    }
}
