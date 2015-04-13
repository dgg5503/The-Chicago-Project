using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;

namespace TheChicagoProject
{
    /// <summary>
    /// Entity Manager handles anything and everything to do with entities.
    /// Collisions, 'ticking', and whatnot.
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public class EntityManager
    {
        private List<Entity.Entity> entities;
        private int playerLoc;
        public Game1 mainGame;
        public Quests.QuestLog quests;
        public World world;

        public List<Entity.Entity> EntityList {
            get { return entities; }
        }

        public EntityManager(Game1 game, World world) {
            entities = new List<Entity.Entity>();
            playerLoc = -1;
            this.mainGame = game;
            this.world = world;
        }

        public void AddEntity(Entity.Entity e) {
            e.currentWorld = world;
            entities.Add(e);
            Console.WriteLine("Added Entity");
            if (e is Player) {
                Console.WriteLine("Added Player");
                playerLoc = entities.Count - 1;
            }
        }

        public Player GetPlayer() {
            return entities[playerLoc] as Player;
        }

        public void Update(GameTime time) {
            for (int x = 0; x < entities.Count; x++) {
                Entity.Entity e = entities[x];
                if (e is LivingEntity && ((LivingEntity) e).health < 1) {
                    entities.Remove(e);
                } else {
                    e.Update(time, this);
                }
            }
        }

        /// <summary>
        /// Check for collisions and return colliding entities.
        /// </summary>
        /// <returns></returns>
        public List<Entity.Entity[]> DoCollisions() {

            List<Entity.Entity[]> list = new List<Entity.Entity[]>();
            foreach (Entity.Entity e1 in entities) {
                foreach (Entity.Entity e2 in entities) {
                    if (e1.location.Intersects(e2.location))
                        list.Add(new Entity.Entity[] { e1, e2 });
                }
            }
            return list;
        }

        /// <summary>
        /// Takes the equation of the bullet a figures out what, if anything, is hit
        /// </summary>
        /// <param name="x">The attacker x location</param>
        /// <param name="y">The attacker y location</param>
        /// <param name="i">The i component of the direction vector</param>
        /// <param name="j">The j component of the direction vector</param>
        public static void FireBullet(float x, float y, double i, double j) {
            Console.WriteLine("Boom");
            //throw new NotImplementedException();
        }
    }
}
