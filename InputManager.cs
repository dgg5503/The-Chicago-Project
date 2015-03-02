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
    class InputManager
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
                WorldManager.player.location.Y -= speed;
            }
            if (keyState.IsKeyDown(Keys.A))    //move down
            {
                WorldManager.player.location.X -= speed;
            }
            if (keyState.IsKeyDown(Keys.S))    //move left
            {
                WorldManager.player.location.Y += speed;
            }
            if (keyState.IsKeyDown(Keys.D))    //move right
            {
                WorldManager.player.location.X += speed;
            }


            if (keyState.IsKeyDown(Keys.Q) || mouseState.MiddleButton == ButtonState.Pressed)    //weapon wheel
            {
                //Display weapon wheel over the screen, goes away when the key is released
            }
            if (keyState.IsKeyDown(Keys.E))    //Interact
            {

            }
            if (keyState.IsKeyDown(Keys.R))    //Reload
            {

            }
            if (keyState.IsKeyDown(Keys.I))    //Inventory
            {

            }
            if (keyState.IsKeyDown(Keys.G))    //Quest log
            {

            }
            if (keyState.IsKeyDown(Keys.D1))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D2))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D3))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D4))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D5))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D6))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D7))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D8))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D9))    //Quick Weapon Select
            {

            }
            if (keyState.IsKeyDown(Keys.D0))    //Quick Weapon Select
            {

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
