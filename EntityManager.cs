using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI.Particles;
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
        public World world;
        public int civilianCount;

        public List<Entity.Entity> EntityList {
            get { return entities; }
        }

        public EntityManager(World world) {
            entities = new List<Entity.Entity>();
            playerLoc = -1;
            this.mainGame = Game1.Instance;
            this.world = world;
        }

        public void AddEntity(Entity.Entity e) {
            e.currentWorld = world;
            Console.WriteLine("Added Entity: " + e.GetType().FullName + " (#" + e.GetHashCode() + ")");
            if (e is Player) {
                if (playerLoc == -1) {
                    playerLoc = 0;
                    entities.Insert(0, e);
                } else {
                    entities[playerLoc] = e;
                }
            } else {
                //if (e is NPC)
                   // civilianCount++;
                entities.Add(e);
            }
        }

        public Player GetPlayer() {
            if (playerLoc < 0) {
                return null;
            }
            return entities[playerLoc] as Player;
        }

        public void Update(GameTime time) {
            for (int x = 0; x < entities.Count; x++) {
                Entity.Entity e = entities[x];
                if (e.markforDelete) {
                    if (e is Player) {
                        Game1.state = GameState.Menu;
                        (e as Player).health = (e as Player).maxHealth;
                        e.markforDelete = false;
                    } else {
                        entities.Remove(e);
                    }
                    //if (e is NPC)
                       // civilianCount--;

                } else
                    e.Update(time, this);
            }
        }

        /// <summary>
        /// Takes the equation of the bullet a figures out what, if anything, is hit
        /// </summary>
        /// <param name="x">The attacker x location</param>
        /// <param name="y">The attacker y location</param>
        /// <param name="i">The i component of the direction vector</param>
        /// <param name="j">The j component of the direction vector</param>
        public void FireBullet(float x, float y, float i, float j, int damage, LivingEntity shooter) {
            Vector2 bullet = new Vector2(x + i, y + j);

            #region Screen Bounds
            int right = WorldManager.player.location.IntX + (RenderManager.ViewportWidth / 2);
            int left = WorldManager.player.location.IntX - (RenderManager.ViewportWidth / 2);
            int top = WorldManager.player.location.IntY - (RenderManager.ViewportHeight / 2);
            int bottom = WorldManager.player.location.IntY + (RenderManager.ViewportHeight / 2);
            #endregion

            bool go = true;
            for (int t = 1; bullet.X < right && bullet.X > left && bullet.Y < bottom && bullet.Y > top && go; t++) {
                int tileX = (int) (bullet.X / GUI.Tile.SIDE_LENGTH);
                int tileY = (int) (bullet.Y / GUI.Tile.SIDE_LENGTH);

                if (mainGame.worldManager.CurrentWorld.tiles.Length <= tileX || mainGame.worldManager.CurrentWorld.tiles[tileX].Length <= tileY) {
                    return;
                }

                if (!mainGame.worldManager.CurrentWorld.tiles[tileX][tileY].IsWalkable && !mainGame.worldManager.CurrentWorld.tiles[tileX][tileY].FileName.Equals("Water.png")) {
                    break;
                }

                for (int cntr = 0; cntr < entities.Count; cntr++) {
                    if (entities[cntr].location.Contains(bullet) && !entities[cntr].Equals(shooter)) {
                        go = false;
                        if (entities[cntr] is LivingEntity) {
                            (entities[cntr] as LivingEntity).health -= damage;
                        } else {
                            break;
                        }
                    }
                }

                bullet.X += i;
                bullet.Y += j;
            }

            // line is from x,y to bullet hit
            Game1.Instance.renderManager.EmitParticle(new Line(Color.LightGoldenrodYellow, 1, new Vector2(x, y), bullet, .05));
        }
    }
}
