//Josiah S DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheChicagoProject.GUI.Forms;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;

namespace TheChicagoProject
{
    /// <summary>
    /// Handles input (in case we end up implementing controllers too).
    /// Otherwise helps out with reading in input.
    /// </summary>
    public class InputManager
    {
        private static KeyboardState previousState;

        private Player player;

        /// <summary>
        /// Makes a new Input manager
        /// </summary>
        public InputManager()
        {
        }

        /// <summary>
        /// What happens with Update?
        /// </summary>
        public void HandleInput(KeyboardState keyState, MouseState mouseState, GameTime time)
        {
            player = Game1.Instance.worldManager.CurrentWorld.manager.GetPlayer();
            if (Game1.state == GameState.Game)
            {
                //Detects if the player wants to move
                #region Movement Input
                int speed = 4;

                //int deltaX = 0;
                //int deltaY = 0;

                //The following statements check the movement keys
                if (keyState.IsKeyDown(Keys.W))    //Move up
                {
                    //deltaY -= speed;
                    player.movement.Y = speed * -1;
                }
                if (keyState.IsKeyDown(Keys.A))    //move down
                {
                    //deltaX -= speed;
                    player.movement.X = speed * -1;
                }
                if (keyState.IsKeyDown(Keys.S))    //move left
                {
                    //deltaY += speed;
                    player.movement.Y = speed;
                }
                if (keyState.IsKeyDown(Keys.D))    //move right
                {
                    //deltaX += speed;
                    player.movement.X = speed;
                }

                if (player.movement.X == 0)
                {
                    if (player.movement.Y < 0)
                    {
                        player.direction = Entity.Direction.Up;
                    }
                    else if (player.movement.Y > 0)
                    {
                        player.direction = Entity.Direction.Down;
                    }
                }
                else if (player.movement.X < 0)
                {
                    if (player.movement.Y < 0)
                    {
                        player.direction = Entity.Direction.UpLeft;
                    }
                    else if (player.movement.Y == 0)
                    {
                        player.direction = Entity.Direction.Left;
                    }
                    else if (player.movement.Y > 0)
                    {
                        player.direction = Entity.Direction.DownLeft;
                    }
                }
                else
                {
                    if (player.movement.Y < 0)
                    {
                        player.direction = Entity.Direction.UpRight;
                    }
                    else if (player.movement.Y == 0)
                    {
                        player.direction = Entity.Direction.Right;
                    }
                    else if (player.movement.Y > 0)
                    {
                        player.direction = Entity.Direction.DownRight;
                    }
                }
                /*
                if (Game1.Instance.worldManager.CurrentWorld.tiles[(int)(player.location.X / GUI.Tile.SIDE_LENGTH)][(int)(player.location.Y / GUI.Tile.SIDE_LENGTH)] is GUI.Door)
                {
                    GUI.Door door = Game1.Instance.worldManager.CurrentWorld.tiles[(int)(player.location.X / GUI.Tile.SIDE_LENGTH)][(int)(player.location.Y / GUI.Tile.SIDE_LENGTH)] as GUI.Door;
                    Game1.Instance.worldManager.current = door.World;
                    player.location.X = door.Destination.X * GUI.Tile.SIDE_LENGTH;
                    player.location.Y = door.Destination.Y * GUI.Tile.SIDE_LENGTH;
                }*/
                #endregion

                Item.Weapon weapon = Game1.Instance.worldManager.CurrentWorld.manager.GetPlayer().inventory.GetEquippedPrimary();
                //Selects the weapon the player wants
                #region Quick Weapon Select
                if (keyState.IsKeyDown(Keys.D1))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 1;
                }
                if (keyState.IsKeyDown(Keys.D2))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 2;
                }
                if (keyState.IsKeyDown(Keys.D3))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 3;
                }
                if (keyState.IsKeyDown(Keys.D4))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 4;
                }
                if (keyState.IsKeyDown(Keys.D5))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 5;
                }
                if (keyState.IsKeyDown(Keys.D6))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 6;
                }
                if (keyState.IsKeyDown(Keys.D7))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 7;
                }
                if (keyState.IsKeyDown(Keys.D8))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 8;
                }
                if (keyState.IsKeyDown(Keys.D9))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 9;
                }
                if (keyState.IsKeyDown(Keys.D0))    //Quick Weapon Select
                {
                    weapon.Reloading = false;
                    player.lastShot = 0D;
                    player.inventory.ActiveWeapon = 0;
                }
                #endregion

                player.cursor = mouseState.Position;

                // Douglas Gliner
                #region mouse aim (move to player?)

                // mouse location relative to CENTER OF SCREEN (not player since those coords are in a different system)
                //float newAngle = 0f;

                /*
                if (mouseState.Position.X != RenderManager.ViewportWidth / 2 || mouseState.Position.Y != RenderManager.ViewportHeight / 2)
                {
                    newAngle = (float)Math.Acos(Vector2.Dot(new Vector2(0, -1), mousePositionVector) / mousePositionVector.Length()); // original angle vector length should always be 1
                }

                // i dont remember how to math.........
                // range of invCos is 0-pi, can i extend it to 2pi using multiplication or whatever? i dont remember ;-;....
                // PERHAPS SLOW DOWN PLAYER IF NOT MOVING IN SAME DIRECTION AS FACING (as an interval of course, otherwise always slow unless at the exact angle.)
                if (mousePositionVector.X < 0)
                    WorldManager.player.faceDirection = newAngle * -1;
                else
                    WorldManager.player.faceDirection = newAngle;
                */

                // Its this simple!!
                Vector2 mousePositionVector = new Vector2(mouseState.Position.X - (RenderManager.ViewportWidth / 2), mouseState.Position.Y - (RenderManager.ViewportHeight / 2));
                player.faceDirection = (float)(Math.Atan2(mousePositionVector.Y, mousePositionVector.X) + (Math.PI / 2));

                #endregion

                //handles mouse input
                if (mouseState.LeftButton == ButtonState.Pressed)   //Primary fire
                {
                    player.Attack(0, player.inventory.GetEquippedPrimary());
                }
            }
            if ((keyState.IsKeyDown(Keys.Q) && !previousState.IsKeyDown(Keys.Q))|| mouseState.MiddleButton == ButtonState.Pressed)    //weapon wheel
            {
                if (Game1.state == GameState.Game)
                {
                    Game1.state = GameState.WeaponWheel;
                }
            }
            if (keyState.IsKeyDown(Keys.E))    //Interact
            {
                player.Interact();
                //Game1.Instance.worldManager.CurrentWorld.playerMap.printMap();
            }
            if (keyState.IsKeyDown(Keys.R))    //Reload
            {
                if (player.inventory.GetEquippedPrimary().LoadedAmmo != player.inventory.GetEquippedPrimary().maxClip && player.inventory.GetEquippedPrimary().Ammo > 0 && !player.inventory.GetEquippedPrimary().Reloading)
                {
                    player.Reload();
                }
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
                {

                    // DEBUG DIALOG TEST
                    
                    //DialogBox test = new DialogBox(new Vector2(200, 150), DialogBoxType.OkCancel, "Test dialog!!", "You've opened the quest log!", new Vector2(300, 300));
                    //test.yesEvent += test_yesEvent;
                    //test.noEvent += test_noEvent;
                    //NPCTalkUI test = new NPCTalkUI(new Vector2(200, 150), "TESTER", "lololo wow ok hi XD!!");
                    //RenderManager.AddDialog(test);
                     
                    Game1.state = GameState.QuestLog;
                }
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
        
        /*
        void test_yesEvent(object sender, EventArgs e)
        {
            Console.WriteLine("WORKED YES!");
        }
        */

        public void WheelInput(KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Escape) || (keyboardState.IsKeyDown(Keys.Q) && previousState.IsKeyUp(Keys.Q)))
            {
                Game1.state = GameState.Game;
            }
            previousState = keyboardState;
        }

        public void PauseInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
            {
                Game1.state = GameState.Game;
            }
            previousState = keyboardState;
        }
    }
}
