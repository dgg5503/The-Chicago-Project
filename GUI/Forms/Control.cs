using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace TheChicagoProject.GUI.Forms
{
    class Control
    {
        // Private list of controls.
        private List<Control> controls;

        // Border texture
        private Texture2D border;

        // Default spriteFont
        private SpriteFont font;

        // When clicked (input manager?)
        public event EventHandler Click;

        // Is this control visible?
        private bool isVisible;

        // Location relative to container
        private Rectangle locAndSize;

        // If no root, then it must be based on global coords.
        public Control parent;

        /// <summary>
        /// Location of this control relative to its current container.
        /// </summary>
        public Point Location { get { return new Point(locAndSize.X, locAndSize.Y); } set { locAndSize = new Rectangle(value.X, value.Y, locAndSize.Width, locAndSize.Height); } }
        /// <summary>
        /// Size of control. Might want to return a new struct called size later on... (?)
        /// </summary>
        public Point Size { get { return new Point(locAndSize.Width, locAndSize.Height); } set { locAndSize = new Rectangle(locAndSize.X, locAndSize.Y, value.X, value.Y); } }
        /// <summary>
        /// The parent of this control, if null then it must be the root.
        /// </summary>
        //public Control Parent { get { return root; } set { root = value; } } 
        /// <summary>
        /// The border texture for controls, only shows if borderEnabled is true.
        /// </summary>
        public Texture2D Border { get { return border; } set { border = value; } }
        /// <summary>
        /// Font for any elements which use one within this control.
        /// </summary>
        public SpriteFont Font { get { return font; } set { font = value; } }
        /// <summary>
        /// Returns whether or not this control is being drawn on screen.
        /// </summary>
        public bool IsVisible { get { return isVisible; } }


        public Control()
        {
            locAndSize = new Rectangle(0, 0, 0, 0);
            controls = new List<Control>();
            isVisible = false;
            parent = null;
        }

        // This is here to make sure the controls within this one are drawn.
        // This should never be called on its own, always with a base.
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            isVisible = true;
            
            foreach (Control c in controls)
                c.Draw(spriteBatch, gameTime);
        }

        public virtual void LoadTextures(GraphicsDevice graphics)
        {
            // WHAT IF RESIZED?????????
            border = new Texture2D(graphics, this.Size.X, this.Size.Y);
            border.CreateBorder(1, Color.Black);

            foreach (Control c in controls)
                c.LoadTextures(graphics);
        }

        // For loading XNB related files...
        public virtual void LoadContent(ContentManager contentManager)
        {
            // PERHAPS MAKE INTERFACE FOR OBJECTS REQUIRING TEXT?
            font = contentManager.Load<SpriteFont>("Font/TimesNewRoman12");

            foreach (Control c in controls)
                c.LoadContent(contentManager);
        }

        // All cases of callbacks are done here.
        public void Update(GameTime gameTime)
        {
            isVisible = false;


            // THIS SHOULD BE HANDLED BY INPUTMANAGER SOMEHOW!!
            MouseState mouseState = Mouse.GetState();

            Rectangle globalControlLoc = new Rectangle(GlobalLocation().X, GlobalLocation().Y, this.Size.X, this.Size.Y);

            if (globalControlLoc.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Click != null)
                {
                    Click(this, EventArgs.Empty);
                }
            }


            foreach (Control c in controls)
                c.Update(gameTime);
        }

        public Point GlobalLocation()
        {
            if (parent == null)
                return this.Location;

            return GlobalLocation(this.Location, parent);
        }

        private Point GlobalLocation(Point location, Control parent)
        {
            if (parent == null)
                return location;

            return location + parent.Location;
        }

        public void Add(Control control)
        {
            controls.Add(control);
        }
    }
}
