﻿using System;
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

        protected GameTime time;

        protected double lastShot;

        public LivingEntity(Rectangle rect, string fileName) : base(rect, fileName) {
            inventory = new List<Item.Item>();
            time = new GameTime();
            lastShot = 0D;
        }

        public override void Update(GameTime time, EntityManager manager) {
            base.Update(time, manager);
            

            this.time = time;
            lastShot += time.ElapsedGameTime.Milliseconds;
        }

        /// <summary>
        /// The Living Entity attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        /// <param name="weapon">The weapon with which they are attacking</param>
        public virtual void Attack(int type, Weapon weapon = null)
        {
            if (type == 0)
            {
                if (lastShot > (1D / (weapon.rateOfFire)) || lastShot < 0D)
                {
                double trajectory = 0D;
                trajectory += 0D;
                EntityManager.FireBullet(location.X, location.Y, System.Math.Cos(trajectory), System.Math.Sin(trajectory));
            }
        }
    }
}
}
