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
    abstract class InteractiveControl : BaseControl
    {
        // When clicked (input manager?) a.k.a released
        public event EventHandler Click;

        public event EventHandler Pressed;

        public event EventHandler Hover;


        MouseState lastFrameMouseState;

        public InteractiveControl(Vector2 size) : base(size) { }
    

        public override void Update(GameTime gameTime)
        {
            // THIS SHOULD BE HANDLED BY INPUTMANAGER SOMEHOW!!
            MouseState mouseState = Mouse.GetState();

            Rectangle globalControlLoc = new Rectangle((int)GlobalLocation().X, (int)GlobalLocation().Y, (int)this.Size.X, (int)this.Size.Y);

            // hover
            if (globalControlLoc.Contains(mouseState.Position))
            {
                if (Hover != null)
                    Hover(this, EventArgs.Empty);

                // pressed
                if (lastFrameMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Pressed != null)
                        Pressed(this, EventArgs.Empty);

                    // released / click
                    if (mouseState.LeftButton == ButtonState.Released && Click != null)
                        Click(this, EventArgs.Empty);
                }
            }

            lastFrameMouseState = mouseState;

            base.Update(gameTime);
        }
    }
}
