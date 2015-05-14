#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.GUI;
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
        Shop,
        Exiting
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private bool screenChanged;
        public RenderManager renderManager;
        public WorldManager worldManager;
        public InputManager inputManager;
        public SaveManager saveManager;
        public CollisionManager collisionManager;
        public Random random;
        #region debug
        public SpriteFont debugFont;
        public static Texture2D border;
        #endregion

        public static GameState state;

        private static Game1 inst;

        public static Game1 Instance {
            get {
                if (inst == null)
                    inst = new Game1();
                return inst;
            }
        }

        private Game1()
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
            random = new Random();
            saveManager = new SaveManager();
            worldManager = new WorldManager();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();
            
            base.Initialize();
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
            renderManager = new RenderManager(spriteBatch, GraphicsDevice, worldManager);

            this.IsMouseVisible = true;
            

            #region debug
            /*
            border = new Texture2D(GraphicsDevice, Tile.SIDE_LENGTH, Tile.SIDE_LENGTH);
            border.CreateBorder(1, Microsoft.Xna.Framework.Color.Black);
             */
            debugFont = Content.Load<SpriteFont>("Font/TimesNewRoman12");
            #endregion

            //Stack Overflow code (see below)
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            base.LoadContent();
        }

        /// <summary>
        /// Code taken from Stack Overflow, because it's amazing!
        /// http://gamedev.stackexchange.com/questions/68914/issue-with-monogame-resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_ClientSizeChanged(object sender, EventArgs e) {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            screenChanged = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            //saveManager.Save();

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
            if (screenChanged) {
                graphics.ApplyChanges();
                screenChanged = false;
            }
            switch (state) {
                case GameState.Menu:
                    break;
                case GameState.Game:
                    /*
                    if(mugTemp == null)
                    {
                        mugTemp = new Mugging("mugging1", "Stop the mugger!", "you have to stop the mugger!", new Vector2(10, 10), worldManager.CurrentWorld.manager.GetPlayer(), worldManager);
                    }

                    if (gameTime.ElapsedGameTime.TotalSeconds > 30 && mugTemp.Status < 2)
                    {
                        mugTemp.StartQuest();
                    }
                    else
                    {
                        //Console.WriteLine("QUEST IN PROGRESS");
                        //Console.WriteLine("MUGGER LOC: {0}, {1}", mugTemp.entitites[0].location.X, mugTemp.entitites[0].location.Y);
                        //Console.WriteLine("PLAYER LOC: {0}, {1}", worldManager.CurrentWorld.manager.GetPlayer().location.X, worldManager.CurrentWorld.manager.GetPlayer().location.Y);
                    }
                    */
                    // need to fix fleemap lag before renabling the above.
                    worldManager.Update(gameTime);
                    collisionManager.Update();
                    inputManager.HandleInput(Keyboard.GetState(), Mouse.GetState(), gameTime);
                    worldManager.CurrentWorld.tick(gameTime); // should only appear here unless ticking while paused (?)
                    
                    //collisionManager.Update();
                    break;
                case GameState.Pause:
                    inputManager.PauseInput(Keyboard.GetState());
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
            GraphicsDevice.Clear(Color.Gray);

            // Everything is drawn with this line (we'll probably pass gameTime in for proper animation...)
            renderManager.Draw(gameTime);

            spriteBatch.Begin();
            if(worldManager.CurrentWorld.manager.GetPlayer() != null)
                spriteBatch.DrawString(debugFont, "(" + worldManager.CurrentWorld.manager.GetPlayer().location.X + ", " + worldManager.CurrentWorld.manager.GetPlayer().location.Y + ")",
                    new Vector2(10f, GraphicsDevice.Viewport.Height - 16),
                    Color.HotPink
                    );
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args) {
            base.OnExiting(sender, args);
            state = GameState.Exiting;
        }
    }
}
