using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

//Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{
    /*
     * Future to-do's
     * - Padding
     * - alternative to containers?
     * - CenterLeft, Right, Horizontal, Vertical
     * - resizing (i.e. build with a size and then scale from that...)
     * - dragging of the window.
     * - Text scaling
     * - Border/fill offset.
     */
    public enum ControlAlignment
    {
        Left,
        Right,
        Center,
        CenterX
    }

    public struct BorderInfo
    {
        public int width;
        //public float offsetX;
        //public float offsetY;
        public Color color;
        public Sprite sprite;
        //public Texture2D texture;
        public bool isDrawn;

        public BorderInfo(int width, Color color)
        {
            this.width = width;
            this.color = color;
            this.sprite = null;
            /*
            this.offsetX = 0;
            this.offsetY = 0;
             * */
            //texture = null;
            this.isDrawn = true;
            if (width < 1)
                this.width = 1;

            if (color == null)
                this.color = Color.Black;
        }

        public BorderInfo(Sprite sprite)
        {
            this.width = 1;
            this.color = Color.White;
            this.sprite = sprite;
            /*
            this.offsetX = 0;
            this.offsetY = 0;
            if(sprite.Texture != null)
            {
                this.offsetX = sprite.Texture.Width / 2;
                this.offsetY = sprite.Texture.Height / 2;
            }*/

            
            isDrawn = true;
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
        // Graphics manager used to load stuff into this control
        private GraphicsDevice graphics;

        // Content manager used to load stuff into this control.
        private ContentManager contentManager;

        // Private list of controls.
        private List<Control> controls;

        // Border texture
        private Texture2D border;
        private BorderInfo borderInfo;

        // Fill within the rectangle.
        private Texture2D fill;
        private FillInfo fillInfo;

        // inactive overlay
        private Texture2D inactiveAlpha;

        // Default spriteFont
        private SpriteFont font;

        // Gametime for this control
        protected GameTime gameTime;

        // Font XNB file.
        private string fontFile;

        // Is this control visible?
        private bool isVisible;

        private bool isDrawn;

        private bool isActive;

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

        public event EventHandler HoverRelease;

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
        public BorderInfo? Border {
            get {
                return borderInfo; 
            } 
            set {
                if (value == null) {
                    borderInfo.isDrawn = false;
                } 
                else {
                    borderInfo = (BorderInfo)value;
                    
                    if (borderInfo.sprite != null && borderInfo.sprite.Texture != null) 
                    {
                        border = borderInfo.sprite.Texture;
                    }
                    else
                        if(border != null)
                            UpdateBorder(); 
                } 
            } 
        } // do OTF load for new border width...
        /// <summary>
        /// Sets the fill of the control to some given Texture2D
        /// </summary>
        //public Texture2D Fill { get { return fill; } set { fill = value; } }
        public FillInfo? Fill { get { return fillInfo; } set { if (value == null) { fillInfo.isDrawn = false; } else { fillInfo = (FillInfo)value; if (fillInfo.texture != null) { fill = fillInfo.texture; } } } } // do OTF load...
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
        /// <summary>
        /// Gets the root control of this control.
        /// </summary>
        public Control RootParent { get { return RootControl(this); } }
        /// <summary>
        /// Returns whether or not this control is being drawn on screen.
        /// </summary>
        public bool IsVisible { get { return isVisible; }}
        /// <summary>
        /// Get or set the controls ability to update.
        /// </summary>
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        /// <summary>
        /// Gets or sets whether or not the control should be drawn.
        /// </summary>
        public bool IsDrawn { get { return isDrawn; } set { isDrawn = value; } }

        public Control(string fontFile = "TimesNewRoman12")
        {
            locAndSize = new Rectangle(0, 0, 0, 0);
            controls = new List<Control>();
            firstClickLoc = Vector2.Zero;
            alignment = ControlAlignment.Left;
            isVisible = true;
            isActive = true;
            isDrawn = true;
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
            if (!isDrawn)
                return;

            isVisible = true;

            if(fillInfo.isDrawn)
                spriteBatch.Draw(fill, this.GlobalLocation(), null, fillInfo.color, 0, Vector2.Zero, new Vector2(Size.X / fill.Width, Size.Y / fill.Height), SpriteEffects.None, 0);
                //spriteBatch.Draw(fill, this.GlobalLocation(), fillInfo.color);
            

            foreach (Control c in controls)
                c.Draw(spriteBatch, gameTime);

            // border offset for placing border outside of control?
            if (borderInfo.isDrawn)
                spriteBatch.Draw(border, this.GlobalLocation(), null, borderInfo.color, 0, Vector2.Zero, new Vector2(Size.X / border.Width, Size.Y / border.Height), SpriteEffects.None, 0);
                //spriteBatch.Draw(border, this.GlobalLocation(), borderInfo.color);

            if (!isActive)
                spriteBatch.Draw(inactiveAlpha, this.GlobalLocation(), Color.White);

            
        }

        protected virtual void LoadTextures(GraphicsDevice graphics)
        {
            if(graphics == null)
            {
                return;
            }
                //throw new Exception("No graphics device defined!");

            // Fill creation
            this.graphics = graphics;

             // update size?
            if (fillInfo.texture == null)
            {
                fill = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                fill.GenColorTexture((int)this.Size.X, (int)this.Size.Y, Color.White);
            }
            else
            {
                fill = fillInfo.texture;
            }

            // Border creation
            //border = borderInfo.texture; // update size?
            
            if (borderInfo.sprite == null)
            {
                border = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                border.CreateBorder(borderInfo.width, Color.White);
            }
            else
            {

                if (borderInfo.sprite.Texture == null)
                {
                    //borderInfo.sprite.Texture = new Texture2D(graphics, borderInfo.sprite.Width, borderInfo.sprite.Height);
                    borderInfo.sprite.Texture = contentManager.Load<Texture2D>("./GUI/" + borderInfo.sprite.FileName);
                }

                border = borderInfo.sprite.Texture;
            }
                

            if (inactiveAlpha == null)
            {
                inactiveAlpha = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                inactiveAlpha.GenColorTexture((int)this.Size.X, (int)this.Size.Y, new Color(Color.Gray, 90));
            }

            foreach (Control c in controls)
                c.LoadTextures(graphics);
        }

        private void UpdateBorder()
        {

            if (borderInfo.sprite == null)
            {
                border = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
                border.CreateBorder(borderInfo.width, Color.White);
            }
            else
            {

                if (borderInfo.sprite.Texture == null)
                {
                    //borderInfo.sprite.Texture = new Texture2D(graphics, borderInfo.sprite.Width, borderInfo.sprite.Height);
                    borderInfo.sprite.Texture = contentManager.Load<Texture2D>("./GUI/" + borderInfo.sprite.FileName);
                }

                border = borderInfo.sprite.Texture;
            }
            /*
            border = new Texture2D(graphics, (int)this.Size.X, (int)this.Size.Y);
            border.CreateBorder(borderInfo.width, Color.White);
             * */
        }

        // For loading XNB related files...
        protected virtual void LoadContent(ContentManager contentManager)
        {
            // PERHAPS MAKE INTERFACE FOR OBJECTS REQUIRING TEXT? (?)
            if(contentManager == null)
                throw new Exception("No content manager defined!");

            this.contentManager = contentManager;

            if(font == null)
                font = contentManager.Load<SpriteFont>("Font/" + fontFile);

            foreach (Control c in controls)
                c.LoadContent(contentManager);
        }


        public void LoadVisuals(ContentManager contentManager = null, GraphicsDevice graphics = null)
        {
            if (contentManager != null)
                LoadContent(contentManager);
            else
                LoadContent(RootControl(this).contentManager);


            if (graphics != null)
                LoadTextures(graphics);
            else
                LoadTextures(RootControl(this).graphics);
        }
        

        // All cases of callbacks are done here.
        public virtual void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (isActive)
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
                        if (Pressed != null && GlobalRectangle.Contains(firstClickLoc))
                            Pressed(this, EventArgs.Empty);

                        // hovered over and release
                        if (lastFrameMouseState.LeftButton == ButtonState.Pressed && currentFrameMouseState.LeftButton == ButtonState.Released)
                            if (HoverRelease != null) // && !GlobalRectangle.Contains(firstClickLoc)
                                HoverRelease(this, EventArgs.Empty);

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
            }

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

                    case ControlAlignment.CenterX:
                        Location = new Vector2((parent.Size.X / 2 - this.Size.X / 2) + this.Location.X, this.Location.Y);
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

                    case ControlAlignment.CenterX:
                        Location = new Vector2(((RenderManager.ViewportWidth / 2) - (this.Size.X / 2)) - this.Location.X, this.Location.Y);
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

                        // needed?
                    case ControlAlignment.CenterX:
                        Location = new Vector2(this.Location.X + RenderManager.ViewportDeltaWidth / 2, this.Location.Y);
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

                    case ControlAlignment.CenterX:
                        Location = new Vector2(this.Location.X + (int)((oldSize.X - this.Size.X) / 2), this.Location.Y); // ints required here to preserve centering
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
            if (borderInfo.sprite == null)
            {
                if (border != null)
                {
                    border = new Texture2D(border.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);
                    border.CreateBorder(borderInfo.width, Color.White);
                }
            }
            else
            {

                if (borderInfo.sprite.Texture == null)
                {
                    //borderInfo.sprite.Texture = new Texture2D(graphics, borderInfo.sprite.Width, borderInfo.sprite.Height);
                    borderInfo.sprite.Texture = contentManager.Load<Texture2D>("./GUI/" + borderInfo.sprite.FileName);
                }

                border = borderInfo.sprite.Texture;
            }
             
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

        /// <summary>
        /// Grabs the root parent of this control.
        /// </summary>
        /// <param name="control">Control to start with.</param>
        /// <returns>The root of the hierarchy.</returns>
        private Control RootControl(Control control)
        {
            if (control.parent != null)
                return RootControl(control.parent);
            return control;
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

            // set parent to this.

            // get other loaded stuff from root control
            control.parent = this;

            /*
            if(control.graphics == null)
                control.graphics = RootControl(control).graphics;

            if(control.contentManager == null)
                control.contentManager = RootControl(control).contentManager;

            // get loaded font from root.
            if(control.font == null)
                control.font = RootControl(this).font;
            */

            // see if controls need to have textures loaded.
            if (RootControl(this).graphics != null && RootControl(this).contentManager != null)
                control.LoadVisuals();

            controls.Add(control);

            if (RootControl(control).gameTime != null)
                control.Update(RootControl(control).gameTime);
            
            /*
            if(RootControl(this).graphics != null && RootControl(this).contentManager != null)
                control.LoadVisuals();
            
            if(RootControl(this).gameTime != null)
                control.Update(RootControl(this).gameTime);
             */
            //controls.Add(control.parent = this);
        }
    }
}
