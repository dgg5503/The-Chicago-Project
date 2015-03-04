using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    class LivingEntity : Entity
    {
        public List<Item.Item> inventory;
        public readonly AI.AI entityAI;

        public LivingEntity(AI.AI ai, int x, int y) {
            this.entityAI = ai;
            this.location = new Vector2(x, y);
            inventory = new List<Item.Item>();
        }
    }
}
