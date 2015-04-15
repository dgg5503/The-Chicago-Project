

//Josiah S DeVizia

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
        private Game1 mainGame;
        private static bool lastFrameFired;
        private static KeyboardState previousState;

        /// <summary>
        /// Makes a new Input manager
        /// </summary>
        /// <param name="mainGame">Instance of the Game1</param>
        public InputManager(Game1 mainGame)
        {
            this.mainGame = mainGame;
            lastFrameFired = true;
        }

        /// <summary>
        /// What happens with Update?
        /// </summary>
        public void HandleInput(KeyboardState keyState, MouseState mouseState, GameTime time)
        {
            if (Game1.state == GameState.Game)
            {
                //Detects if the player wants to move
                #region Movement Input
                int speed = 2;

                //int deltaX = 0;
                //int deltaY = 0;

                //The following statements check the movement keys
                if (keyState.IsKeyDown(Keys.W))    //Move up
                {
                    //deltaY -= speed;
                    WorldManager.player.movement.Y = speed * -1;
                }
                if (keyState.IsKeyDown(Keys.A))    //move down
                {
                    //deltaX -= speed;
                    WorldManager.player.movement.X = speed * -1;
                }
                if (keyState.IsKeyDown(Keys.S))    //move left
                {
                    //deltaY += speed;
                    WorldManager.player.movement.Y = speed;
                }
                if (keyState.IsKeyDown(Keys.D))    //move right
                {
                    //deltaX += speed;
                    WorldManager.player.movement.X = speed;
                }

                if (WorldManager.player.movement.X == 0)
                {
                    if (WorldManager.player.movement.Y < 0)
                    {
                        WorldManager.player.direction = Entity.Direction.Up;
                    }
                    else if (WorldManager.player.movement.Y > 0)
                    {
                        WorldManager.player.direction = Entity.Direction.Down;
                    }
                }
                else if (WorldManager.player.movement.X < 0)
                {
                    if (WorldManager.player.movement.Y < 0)
                    {
                        WorldManager.player.direction = Entity.Direction.UpLeft;
                    }
                    else if (WorldManager.player.movement.Y == 0)
                    {
                        WorldManager.player.direction = Entity.Direction.Left;
                    }
                    else if (WorldManager.player.movement.Y > 0)
                    {
                        WorldManager.player.direction = Entity.Direction.DownLeft;
                    }
                }
                else
                {
                    if (WorldManager.player.movement.Y < 0)
                    {
                        WorldManager.player.direction = Entity.Direction.UpRight;
                    }
                    else if (WorldManager.player.movement.Y == 0)
                    {
                        WorldManager.player.direction = Entity.Direction.Right;
                    }
                    else if (WorldManager.player.movement.Y > 0)
                    {
                        WorldManager.player.direction = Entity.Direction.DownRight;
                    }
                }

                //Selects the weapon the player wants
                #region Quick Weapon Select
                if (keyState.IsKeyDown(Keys.D1))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 1;
                }
                if (keyState.IsKeyDown(Keys.D2))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 2;
                }
                if (keyState.IsKeyDown(Keys.D3))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 3;
                }
                if (keyState.IsKeyDown(Keys.D4))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 4;
                }
                if (keyState.IsKeyDown(Keys.D5))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 5;
                }
                if (keyState.IsKeyDown(Keys.D6))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 6;
                }
                if (keyState.IsKeyDown(Keys.D7))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 7;
                }
                if (keyState.IsKeyDown(Keys.D8))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 8;
                }
                if (keyState.IsKeyDown(Keys.D9))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 9;
                }
                if (keyState.IsKeyDown(Keys.D0))    //Quick Weapon Select
                {
                    WorldManager.player.inventory.ActiveWeapon = 0;
                }
                #endregion

                WorldManager.player.cursor = mouseState.Position;

                //handles mouse input
                if (mouseState.LeftButton == ButtonState.Pressed)   //Primary fire
                {
                    if (!lastFrameFired)
                    {
                        WorldManager.player.Attack(0, WorldManager.player.inventory.GetEquippedPrimary());
                        lastFrameFired = true;
                    }
                    else
                    {
                        lastFrameFired = false;
                    }
                }
                else if (mouseState.RightButton == ButtonState.Pressed)  //grenade
                {
                    throw new NotImplementedException();
                    //WorldManager.player.Attack(1, );
                }
                #endregion
            }
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
                if (Game1.state == GameState.Game || Game1.state == GameState.Pause || Game1.state == GameState.QuestLog)
                {
                    Game1.state = GameState.Inventory;
                }
            }
            if (keyState.IsKeyDown(Keys.G))    //Quest log
            {
                if (Game1.state == GameState.Game || Game1.state == GameState.Pause || Game1.state == GameState.Inventory)
                    Game1.state = GameState.QuestLog;
            }
            if (keyState.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))    //Escape
            {
                if (Game1.state == GameState.Game || Game1.state == GameState.Inventory || Game1.state == GameState.QuestLog)
                {
                    Game1.state = GameState.Pause;
                }
                else if (Game1.state == GameState.Pause)
                {
                    Game1.state = GameState.Game;
                }
            }

            previousState = keyState;
        }
    }
}
