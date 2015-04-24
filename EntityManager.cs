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
        public static Game1 mainGame;
        public Quests.QuestLog quests;
        public World world;

        public List<Entity.Entity> EntityList {
            get { return entities; }
        }

        public EntityManager(Game1 game, World world) {
            entities = new List<Entity.Entity>();
            playerLoc = -1;
            mainGame = game;
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
        /// Takes the equation of the bullet a figures out what, if anything, is hit
        /// </summary>
        /// <param name="x">The attacker x location</param>
        /// <param name="y">The attacker y location</param>
        /// <param name="i">The i component of the direction vector</param>
        /// <param name="j">The j component of the direction vector</param>
        public void FireBullet(float x, float y, float i, float j, int damage) {
            Vector2 bullet = new Vector2(x, y);

            #region Screen Bounds
            int right = WorldManager.player.location.IntX + (RenderManager.ViewportWidth / 2);
            int left = WorldManager.player.location.IntX - (RenderManager.ViewportWidth / 2);
            int top  = WorldManager.player.location.IntY - (RenderManager.ViewportHeight / 2);
            int bottom = WorldManager.player.location.IntY + (RenderManager.ViewportHeight / 2);
            #endregion

            bool go = true;
            for (int t = 1; bullet.X < right && bullet.X > left && bullet.Y < bottom && bullet.Y > top && go; t++)
            {
                int tileX = (int)(bullet.X / GUI.Tile.SIDE_LENGTH);
                int tileY = (int)(bullet.Y / GUI.Tile.SIDE_LENGTH);
                
                if(!mainGame.worldManager.CurrentWorld.tiles[tileX][tileY].IsWalkable)
                {
                    break;
                }
                
                for(int cntr = 0; cntr < entities.Count; cntr ++)
                {
                    if(entities[cntr].location.Contains(bullet))
                    {
                        go = false;
                        if(entities[cntr] is LivingEntity && !(entities[cntr] is Player))
                        {
                            (entities[cntr] as LivingEntity).health -= damage;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                bullet.X += i;
                bullet.Y += j;
            }
        }
    }
}
