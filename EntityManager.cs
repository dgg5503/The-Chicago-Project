using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;

namespace TheChicagoProject
{
    /// <summary>
    /// Entity Manager handles anything and everything to do with entities.
    /// Collisions, 'ticking', and whatnot.
    /// </summary>
    class EntityManager
    {
        private List<Entity.Entity> entities;
        private int playerLoc;

        public List<Entity.Entity> EntityList {
            get { return entities; }
        }

        public EntityManager() {
            entities = new List<Entity.Entity>();
            playerLoc = -1;
        }

        public void AddEntity(Entity.Entity e) {
            entities.Add(e);
            if (e is Player)
                playerLoc = entities.Count - 1;
        }

        public Player GetPlayer() {
            return entities[playerLoc] as Player;
        }

        /// <summary>
        /// Check for collisions and return colliding entities.
        /// </summary>
        /// <returns></returns>
        public List<Entity.Entity[]> DoCollisions() {
            List<Entity.Entity[]> list = new List<Entity.Entity[]>();
            foreach(Entity.Entity e1 in entities) {
                foreach (Entity.Entity e2 in entities) {
                    if (e1.location.Intersects(e2.location))
                        list.Add(new Entity.Entity[] { e1, e2 });
                }
            }
            return list;
        }
    }
}
