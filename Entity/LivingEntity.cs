using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    public class LivingEntity : Entity
    {
        public List<Item.Item> inventory;
        public int health;

        public LivingEntity( Rectangle rect, string fileName) : base(rect, fileName) {
            inventory = new List<Item.Item>();
        }

        public override void Update(GameTime time, EntityManager manager) {
            base.Update(time, manager);
        }

        /// <summary>
        /// The player attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        public virtual void Attack(int type)
        {
            if (type == 0)
            {
                double trajectory = 0D;
                trajectory += 0D;
                EntityManager.FireBullet(location.X, location.Y, System.Math.Cos(trajectory), System.Math.Sin(trajectory));
            }
        }
    }
}
