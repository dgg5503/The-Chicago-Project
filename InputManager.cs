using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

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
        public void HandleInput(KeyboardState keyState, MouseState mouseState)
        {
            float speed = 5f;
            //The following statements check the movement keys
            if (keyState.IsKeyDown(Keys.W))    //Move up
            {
                WorldManager.player.location.Y -= (int)speed;
            }
            if (keyState.IsKeyDown(Keys.A))    //move down
            {
                WorldManager.player.location.X -= (int)speed;
            }
            if (keyState.IsKeyDown(Keys.S))    //move left
            {
                WorldManager.player.location.Y += (int)speed;
            }
            if (keyState.IsKeyDown(Keys.D))    //move right
            {
                WorldManager.player.location.X += (int)speed;
            }


            if (keyState.IsKeyDown(Keys.Q) || mouseState.MiddleButton == ButtonState.Pressed)    //weapon wheel
            {
                WorldManager.player.Interact();
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
            if (keyState.IsKeyDown(Keys.Escape))    //Quest log
            {
                if (Game1.state == GameState.Game)
                    Game1.state = GameState.Pause;
                else
                    Game1.state = GameState.Game;
            }

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
