#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.Quests;
#endregion

namespace TheChicagoProject
{
    //Josia DeVizia
    //Ashin Ganapathiraju
    //Doug Gliner
    //Sean Levorse
    /*
     * Update:
     * Input
     * Entity
     * 
     * Draw:
     * Render
     */

    public enum GameState
    {
        Menu,
        Game,
        Pause,
        Inventory,
        QuestLog,
        WeaponWheel,
        FastTravel,
        Shop
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public RenderManager renderManager;
        public WorldManager worldManager;
        public InputManager inputManager;
        public SaveManager saveManager;

        public static GameState state;
        Mugging mugTemp;

        public Game1()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            state = GameState.Menu;
            saveManager = new SaveManager(this);
            worldManager = new WorldManager(this);
            inputManager = new InputManager(this);
            base.Initialize();

            mugTemp = new Mugging("mugging1", "Stop the mugger!", "you have to stop the mugger!", new Vector2(10, 10), worldManager.CurrentWorld.manager.GetPlayer());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // RenderManager is created here
            // In the constructor for RenderManager, ALL TEXTURES ARE LOADED.
            renderManager = new RenderManager(spriteBatch, GraphicsDevice, this, worldManager);

            this.IsMouseVisible = true;
            //Load the data
            //saveManager.Load();//Currently Throws a not implemented exception

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            saveManager.Save();

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            */
            //FSM
            switch (state) {
                case GameState.Menu:
                    break;
                case GameState.Game:
                    if(mugTemp.Status == 1)
                        mugTemp.StartQuest();
                    inputManager.HandleInput(Keyboard.GetState(), Mouse.GetState(), gameTime);
                    worldManager.CurrentWorld.tick(gameTime);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Inventory:
                    break;
                case GameState.QuestLog:
                    break;
                case GameState.WeaponWheel:
                    break;
                case GameState.FastTravel:
                    break;
                case GameState.Shop:
                    break;
                default:
                    break;
            }

            // For sprite and GUI animations
            renderManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Everything is drawn with this line (we'll probably pass gameTime in for proper animation...)
            renderManager.Draw(gameTime);
            
            base.Draw(gameTime);
        }
    }
}
