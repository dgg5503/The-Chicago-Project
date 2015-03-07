using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TheChicagoProject
{
    /// <summary>
    /// Handles input (in case we end up implementing controllers too).
    /// Otherwise helps out with reading in input.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// What happens with Update?
        /// </summary>
        public void HandleInput(KeyboardState keyState, MouseState mouseState, GameTime time)
        {
            #region Movement Input
            int speed = 5;

            int deltaX = 0;
            int deltaY = 0;

            //The following statements check the movement keys
            if (keyState.IsKeyDown(Keys.W))    //Move up
            {
                deltaY -= speed;
            }
            if (keyState.IsKeyDown(Keys.A))    //move down
            {
                deltaX -= speed;
            }
            if (keyState.IsKeyDown(Keys.S))    //move left
            {
                deltaY += speed;
            }
            if (keyState.IsKeyDown(Keys.D))    //move right
            {
                deltaX += speed;
            }

            if (deltaX == 0 && deltaY < 0)
            {
                Entity.Player.direction = Entity.Direction.Up;
            }
            else if (deltaX > 0 && deltaY < 0)
            {
                Entity.Player.direction = Entity.Direction.UpRight;
            }
            else if (deltaX > 0 && deltaY == 0)
            {
                Entity.Player.direction = Entity.Direction.Right;
            }
            else if (deltaX > 0 && deltaY > 0)
            {
                Entity.Player.direction = Entity.Direction.DownRight;
            }
            else if (deltaX == 0 && deltaY > 0)
            {
                Entity.Player.direction = Entity.Direction.Down;
            }
            else if (deltaX < 0 && deltaY > 0)
            {
                Entity.Player.direction = Entity.Direction.DownLeft;
            }
            else if (deltaX < 0 && deltaY == 0)
            {
                Entity.Player.direction = Entity.Direction.Left;
            }
            else if (deltaX < 0 && deltaY < 0)
            {
                Entity.Player.direction = Entity.Direction.UpLeft;
            }

            int x = WorldManager.player.location.X;
            int y = WorldManager.player.location.Y;

            x += deltaX * time.ElapsedGameTime.Milliseconds;
            y += deltaY * time.ElapsedGameTime.Milliseconds;

            WorldManager.player.location.X = x;
            WorldManager.player.location.Y = y;
            #endregion

            if (keyState.IsKeyDown(Keys.Q) || mouseState.MiddleButton == ButtonState.Pressed)    //weapon wheel
            {
                if(Game1.state == GameState.Game)
                {
                    Game1.state = GameState.WeaponWheel;
                }
            }
            if (keyState.IsKeyDown(Keys.E))    //Interact
            {
                WorldManager.player.Interact();
            }
            if (keyState.IsKeyDown(Keys.R))    //Reload
            {
                WorldManager.player.Reload();
            }
            if (keyState.IsKeyDown(Keys.I))    //Inventory
            {
                Game1.state = GameState.Inventory;
            }
            if (keyState.IsKeyDown(Keys.G))    //Quest log
            {
                Game1.state = GameState.QuestLog;
            }
            if (keyState.IsKeyDown(Keys.Escape))    //Escape
            {
                if (Game1.state == GameState.Game)
                {
                    Game1.state = GameState.Pause;
                }
                else if (Game1.state != GameState.Menu && Game1.state != GameState.FastTravel && Game1.state != GameState.Shop)
                {
                    Game1.state = GameState.Game;
                }
            }

            #region Quick Weapon Select
            if (keyState.IsKeyDown(Keys.D1))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 1;
            }
            if (keyState.IsKeyDown(Keys.D2))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 2;
            }
            if (keyState.IsKeyDown(Keys.D3))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 3;
            }
            if (keyState.IsKeyDown(Keys.D4))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 4;
            }
            if (keyState.IsKeyDown(Keys.D5))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 5;
            }
            if (keyState.IsKeyDown(Keys.D6))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 6;
            }
            if (keyState.IsKeyDown(Keys.D7))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 7;
            }
            if (keyState.IsKeyDown(Keys.D8))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 8;
            }
            if (keyState.IsKeyDown(Keys.D9))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 9;
            }
            if (keyState.IsKeyDown(Keys.D0))    //Quick Weapon Select
            {
                WorldManager.player.ActiveWeapon = 0;
            }
            #endregion

            //handles mouse input
            if (mouseState.LeftButton == ButtonState.Pressed)   //Primary fire
            {
                WorldManager.player.Attack(0);
            }
            else if (mouseState.RightButton == ButtonState.Pressed)  //grenade
            {
                WorldManager.player.Attack(1);
            }
        }
    }
}
