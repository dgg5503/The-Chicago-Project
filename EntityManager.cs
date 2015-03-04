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
    }
}
