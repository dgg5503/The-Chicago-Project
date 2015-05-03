using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// Keeps his distance.
    /// Will attempt to shoot you / grenade you.
    /// Stays within 5-7 tiles, and moves away if you get close.
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public class MidAI : AI
    {
        public MidAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap playerMap = manager.world.playerMap;
            DijkstraMap fleeMap = manager.world.fleeMap;
            // Floats --> Ints (D.G. 4/5/15, 920pm)
            int pX = (int) (entity.location.Center.X / Tile.SIDE_LENGTH) - playerMap.modX;
            int pY = (int) (entity.location.Center.Y / Tile.SIDE_LENGTH) - playerMap.modX;
            if (pX < 0 || pY < 0 || pX >= playerMap.Map.Length || pY >= playerMap.Map[0].Length)
                return;
            int dist = playerMap.Map[pX][pY];
            Direction furtherDir = findPos(fleeMap, 1);
            Direction closerDir = findPos(playerMap, 1);
            //System.Diagnostics.Debug.WriteLine("pM: " + dist + "\tfM: " + fleeMap.Map[pX][pY]);
            if (dist < 3) {
                entity.direction = furtherDir;
                entity.Move();
            } else if (dist < 8) {
                entity.direction = closerDir;
                entity.Attack(0, entity.inventory.GetEquippedPrimary());
                //System.Diagnostics.Debug.WriteLine("Firing at player. " + entity.direction);
            } else {
                entity.direction = closerDir;
                entity.Move();
            }
        }
    }
}
