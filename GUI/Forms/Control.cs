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
    /*
     * Future to-do's
     * - Padding
     * - alternative to containers?
     * - CenterLeft, Right, Horizontal, Vertical
     * - resizing (i.e. build with a size and then scale from that...)
     * - dragging of the window.
     * - 
     */

    //Douglas Gliner
    public enum ControlAlignment
    {
        Left,
        Right,
        Center
    }

    public struct BorderInfo
    {
        public int width;
        public Color color;
        public Texture2D texture;
        public bool isDrawn;

        public BorderInfo(int width, Color color)
        {
            this.width = width;
            this.color = color;
            texture = null;
            isDrawn = true;
            if (width < 1)
                width = 1;

            if (color == null)
                color = Color.Black;
        }

        public BorderInfo(Texture2D texture)
        {
            width = 0;
            color = Color.White;
            this.texture = texture;
            isDrawn = true;
            if(texture == null)
            {
                width = 1;
                color = Color.Black;
            }
        }
    }

    public struct FillInfo
    {
        public Color color;
        public Texture2D texture;
        public bool isDrawn;

        public FillInfo(Color color)
        {
            this.color = color;
            isDrawn = true;
            if (color == null)
                color = Color.Gray;
            
            texture = null;
        }

        public FillInfo(Texture2D texure, Color color)
        {
            this.texture = texure;
            this.color = color;
            isDrawn = true;

            if (texure == null)
                this.color = Color.Gray;
        }
    }

    // Parent should be viewport by default...

    // Douglas Gliner
    public abstract class Control
    {
        // Private list of controls.
        private List<Control> controls;

        // Border texture
        private Texture2D border;
        private BorderInfo borderInfo;

        // Fill within the rectangle.
        private Texture2D fill;
        private FillInfo fillInfo;

        // Default spriteFont
        private SpriteFont font;

        // Font XNB file.
        private string fontFile;

        // Is this control visible?
        private bool isVisible;

        // Location relative to container
        private Rectangle locAndSize;

        // If no root, then it must be based on global coords.
        public Control parent;

        // Alignment relative to parent
        private ControlAlignment alignment;

        // Alignment applies
        protected bool alignApplied;

        // When clicked (input manager?)
        public event EventHandler Click;

        public event EventHandler Pressed;

        public event EventHandler Hover;

        MouseState lastFrameMouseState;

        MouseState currentFrameMouseState;

        private Vector2 firstClickLoc;

        /// <summary>
        /// Location of this control relative to its current container.
        /// </summary>
        public Vector2 Location { get { return new Vector2(locAndSize.X, locAndSize.Y); } set { locAndSize = new Rectangle((int)value.X, (int)value.Y, locAndSize.Width, locAndSize.Height); } }
        /// <summary>
        /// Size of control. Might want to return a new struct called size later on... (?)
        /// </summary>
        public Vector2 Size { get { return new Vector2(locAndSize.Width, locAndSize.Height); } set { locAndSize = new Rectangle(locAndSize.X, locAndSize.Y, (int)value.X, (int)value.Y); } }
        /// <summary>
        /// Returns rectangle location relative to viewport.
        /// </summary>
        public Rectangle GlobalRectangle { get { return new Rectangle((int)GlobalLocation().X, (int)GlobalLocation().Y, (int)this.Size.X, (int)this.Size.Y); } }
        /// <summary>
        /// The parent of this control, if null then it must be the root.
        /// </summary>
        //public Control Parent { get { return root; } set { root = value; } } 
        /// <summary>
        /// The border texture for controls, only shows if borderEnabled is true.
        /// </summary>
        //public Texture2D Border { get { return border; } set { border = value; } }
        public BorderInfo? Border { get { return borderInfo; } set { if (value == null) { borderInfo.isDrawn = false; } else { borderInfo = (BorderInfo)value; } } } // do OTF load...
        /// <summary>
        /// Sets the fill of the control to some given Texture2D
        /// </summary>
        //public Texture2D Fill { get { return fill; } set { fill = value; } }
        public FillInfo? Fill { get { return fillInfo; } set { if (value == null) { fillInfo.isDrawn = false; } else { fillInfo = (FillInfo)value; } } } // do OTF load...
        /// <summary>
        /// Gets this frames mouse state
        /// </summary>
        public MouseState CurrentFrameMouseState { get { return currentFrameMouseState; } }
        /// <summary>
        /// Returns list of controls found on this control.
        /// </summary>
        public List<Control> Controls { get { return controls; } }
        /// <summary>
        /// Font for any elements which use one within this control.
        /// </summary>
        public SpriteFont Font { get { return font; } set { font = value; } }
        /// <summary>
        /// Sets the alignment of this control relative to its parent.
        /// </summary>
        public ControlAlignment Alignment { get { return alignment; } set { alignment = value; } }
        //public bool RequiresOTFLoad { get { if(fill == null || border == null){return true;} return false; } }
        /// <summary>
        /// Returns whether or not this control is being drawn on screen.
        /// </summary>
        public bool IsVisible { get { return isVisible; }}

        public Control(string fontFile = "TimesNewRoman12")
        {
            locAndSize = new Rectangle(0, 0, 0, 0);
            controls = new List<Control>();
            firstClickLoc = Vector2.Zero;
            alignment = ControlAlignment.Left;
            isVisible = true;
            alignApplied = false;
            parent = null;
            this.fontFile = fontFile;
            borderInfo = new BorderInfo(1, Color.Black);
            fillInfo = new FillInfo(Color.Gray);

        }

        // This is here to make sure the controls within this one are drawn.
        // This should never be called on its own, always with a base.
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            isVisible = true;

            if(fillInfo.isDrawn)
                spriteBatch.Draw(fill, this.GlobalLocation(), fillInfo.color);

            foreach (Control c in controls)
                c.Draw(spriteBatch, gameTime);

            if (borderInfo.isDrawn)
                spriteBatch.Draw(border, this.GlobalLocation(), borderInfo.color);  
        }

        protected virtual void LoadTextures(GraphicsDevice graphics)
        {
            // Fill creation
            if (fillInfo.texture == null)
            {
                fill = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                fill.GenColorTexture((int)this.Size.X, (int)this.Size.Y, Color.White);
            }
            else
                fill = fillInfo.texture; // update size?

            // Border creation
            if (borderInfo.texture == null)
            {
                border = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                border.CreateBorder(borderInfo.width, Color.White); 
            }
            else
                border = borderInfo.texture; // update size?

            foreach (Control c in controls)
                c.LoadTextures(graphics);
        }

        // For loading XNB related files...
        protected virtual void LoadContent(ContentManager contentManager)
        {
            // PERHAPS MAKE INTERFACE FOR OBJECTS REQUIRING TEXT? (?)
            font = contentManager.Load<SpriteFont>("Font/" + fontFile);

            foreach (Control c in controls)
                c.LoadContent(contentManager);
        }


        public void LoadVisuals(ContentManager contentManager = null, GraphicsDevice graphics = null)
        {
            if (contentManager != null)
                LoadContent(contentManager);

            if (graphics != null)
                LoadTextures(graphics);
        }
        

        // All cases of callbacks are done here.
        public virtual void Update(GameTime gameTime)
        {
            currentFrameMouseState = Mouse.GetState();

            if (currentFrameMouseState.LeftButton == ButtonState.Pressed)
                if (firstClickLoc == Vector2.Zero)
                    firstClickLoc = new Vector2(currentFrameMouseState.Position.X, currentFrameMouseState.Position.Y);

            // hover
            if (GlobalRectangle.Contains(currentFrameMouseState.Position))
            {
                if (Hover != null)
                    Hover(this, EventArgs.Empty);

                // pressed
                if (lastFrameMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Pressed != null)
                        Pressed(this, EventArgs.Empty);

                    // released / click
                    if (currentFrameMouseState.LeftButton == ButtonState.Released)
                    {
                        if (Click != null && GlobalRectangle.Contains(firstClickLoc))
                            Click(this, EventArgs.Empty);
                    }
                    
                }
            }

            if (currentFrameMouseState.LeftButton == ButtonState.Released)
                firstClickLoc = Vector2.Zero;

            lastFrameMouseState = currentFrameMouseState;

            if (!alignApplied)
            {
                ControlAlign();
                ParentAlign();
                alignApplied = true;
            }
            
            foreach (Control c in controls)
                c.Update(gameTime);

            isVisible = false;
        }

        /// <summary>
        /// Aligns control relative to parent.
        /// </summary>
        private void ControlAlign()
        {
            if (this.parent != null)
            {
                switch (alignment)
                {
                    case ControlAlignment.Center:
                        Location = new Vector2((parent.Size.X / 2 - this.Size.X / 2) + this.Location.X, (parent.Size.Y / 2 - this.Size.Y / 2) + this.Location.Y);
                        break;

                    case ControlAlignment.Left:
                        Location = new Vector2(this.Location.X, this.Location.Y);
                        break;

                    case ControlAlignment.Right:
                        Location = new Vector2((parent.Size.X - this.Size.X) - this.Location.X, this.Location.Y);
                        break;
                }
            }
        }

        /// <summary>
        /// Aligns control relative to viewport.
        /// </summary>
        private void ParentAlign()
        {
            if (this.parent == null)
            {
                switch (alignment)
                {
                    case ControlAlignment.Center:
                        Location = new Vector2(((RenderManager.ViewportWidth / 2) - (this.Size.X / 2)) - this.Location.X, ((RenderManager.ViewportHeight / 2) - (this.Size.Y / 2)) - this.Location.Y);
                        break;

                    case ControlAlignment.Left:
                        Location = new Vector2(this.Location.X, this.Location.Y);
                        break;

                    case ControlAlignment.Right:
                        Location = new Vector2((RenderManager.ViewportWidth - this.Size.X - this.Location.X), this.Location.Y);
                        break;
                }
            }
        }

        /// <summary>
        /// Moves root control based on new screen size, controls location derive from root so no need to touch those locations.
        /// </summary>
        private void ScreenSizeChangeAlignParent()
        {
            if (this.parent == null)
            {
                switch (alignment)
                {
                    case ControlAlignment.Center:
                        Location = new Vector2(this.Location.X + RenderManager.ViewportDeltaWidth / 2, this.Location.Y + RenderManager.ViewportDeltaHeight / 2);
                        break;

                    case ControlAlignment.Left:
                        Location = new Vector2(this.Location.X, this.Location.Y);
                        break;

                    case ControlAlignment.Right:
                        Location = new Vector2(this.Location.X + RenderManager.ViewportDeltaWidth, this.Location.Y + RenderManager.ViewportDeltaHeight);
                        break;
                }
            }
        }

        /// <summary>
        /// Moves root control based on new screen size, controls location derive from root so no need to touch those locations.
        /// </summary>
        private void ControlSizeChangeAlignControl(Vector2 oldSize)
        {
            if (this.parent != null)
            {
                switch (alignment)
                {
                    case ControlAlignment.Center:
                        Location = new Vector2(this.Location.X + (int)((oldSize.X - this.Size.X) / 2), this.Location.Y + (int)((oldSize.Y - this.Size.Y) / 2)); // ints required here to preserve centering
                        break;

                    case ControlAlignment.Left:
                        Location = new Vector2(this.Location.X, this.Location.Y);
                        break;

                    case ControlAlignment.Right:
                        Location = new Vector2(this.Location.X + (oldSize.X - this.Size.X), this.Location.Y + (oldSize.Y - this.Size.Y)); // ints needed here?
                        break;
                }
            }
        }

        // Screen size change reaction
        public void ScreenSizeChange()
        {
            ScreenSizeChangeAlignParent();
        }

        public void ControlSizeChange(Vector2 lastSize)
        {
            ControlSizeChangeAlignControl(lastSize);

            // REMAKE BORDERS AND STUFF
            // Fill creation
            if (this.Size == Vector2.Zero)
                return;

            if (fillInfo.texture == null)
            {
                if (fill != null)
                {
                    fill = new Texture2D(fill.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);
                    fill.GenColorTexture((int)this.Size.X, (int)this.Size.Y, Color.White);
                }
            }
            else
                fill = fillInfo.texture; // update size?

            // Border creation
            if (borderInfo.texture == null)
            {
                if (border != null)
                {
                    border = new Texture2D(border.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);
                    border.CreateBorder(borderInfo.width, Color.White);
                }
            }
            else
                border = borderInfo.texture; // update size?
             
            // REMAKE BORDERS AND STUFF
        }

        public Vector2 GlobalLocation()
        {
            if (parent == null)
                return this.Location;

            return GlobalLocation(this.Location, parent);
        }

        private Vector2 GlobalLocation(Vector2 location, Control parent)
        {
            if (parent == null)
                return location;

            return GlobalLocation(location + parent.Location, parent.parent);
        }

        public virtual void Clear()
        {
            controls.Clear();
        }

        public void Add(Control control)
        {
            // Prevents infinite looping. If for some reason this does occur, throw exception because itll crash the game anyway
            if (control == this)
                throw new InvalidOperationException();

            controls.Add(control);
        
            //controls.Add(control.parent = this);
        }
    }
}
