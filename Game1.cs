#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace TheChicagoProject
{
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

            base.Initialize();

            state = GameState.Menu;
            saveManager = new SaveManager(this);
            worldManager = new WorldManager(this);
            inputManager = new InputManager();
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

            //Load the data
            //saveManager.Load();//Currently Throws a not implemented exception

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            saveManager.Save();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            inputManager.HandleInput(Keyboard.GetState(), Mouse.GetState(), gameTime);
            worldManager.CurrentWorld.tick(gameTime);

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
            spriteBatch.Begin();
            renderManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
