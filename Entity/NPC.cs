//Ashwin Ganapathiraju

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Particles;
using TheChicagoProject.Collision;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    class NPC : LivingEntity
    {
        public NPC(FloatRectangle rect, Sprite sprite, int health) :
            base(rect, sprite, health, null) {
            this.ai = new CivilianAI(this);
        }

        public override void Move() {
            float move = 1.5f;
            switch (direction) {
                case Direction.Down:
                    movement = new Vector2(0, move);
                    this.faceDirection = 2 * (float) Math.PI / 2f;
                    break;
                case Direction.Up:
                    movement = new Vector2(0, -move);
                    this.faceDirection = 0 * (float) Math.PI / 2f;
                    break;
                case Direction.Left:
                    movement = new Vector2(-move, 0);
                    this.faceDirection = 3 * (float) Math.PI / 2f;
                    break;
                case Direction.Right:
                    movement = new Vector2(move, 0);
                    this.faceDirection = 1 * (float) Math.PI / 2f;
                    break;

                case Direction.DownLeft:
                    movement = new Vector2(-move, move);
                    this.faceDirection = 5 * (float) Math.PI / 4f;
                    break;
                case Direction.DownRight:
                    movement = new Vector2(move, move);
                    this.faceDirection = 3 * (float) Math.PI / 4f;
                    break;
                case Direction.UpLeft:
                    movement = new Vector2(-move, -move);
                    this.faceDirection = 7 * (float) Math.PI / 4f;
                    break;
                case Direction.UpRight:
                    movement = new Vector2(move, -move);
                    this.faceDirection = 1 * (float) Math.PI / 4f;
                    break;
            }
            //Player player = Game1.Instance.worldManager.CurrentWorld.manager.GetPlayer();
            //float actual = (float) Math.Atan2(this.location.Center.Y - player.location.Center.Y, this.location.Center.X - player.location.Center.X);
            //actual -= (float) Math.PI / 2f;
            //this.faceDirection = actual;
        }
    }
}
