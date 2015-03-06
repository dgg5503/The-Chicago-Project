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
        public int health;

        public LivingEntity(AI.AI ai, Rectangle rect) {
            this.entityAI = ai;
            this.location = rect;
            inventory = new List<Item.Item>();
        }
    }
}
