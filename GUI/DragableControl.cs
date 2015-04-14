using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheChicagoProject.GUI.Forms
{
    public class DragableControl : Control
    {
        private Label textLbl;

        private Vector2 mouseOrigin;

        private bool isPressed;

        public DragableControl()
        {
            this.Location = new Vector2(0, 0);
            this.Size = new Vector2(64, 64);
            this.Pressed += DragableControl_Pressed;

            isPressed = false;
            mouseOrigin = new Vector2(-1, -1);

            InitializeForms();
        }

        void DragableControl_Pressed(object sender, EventArgs e)
        {
            if (isPressed == false)
            {
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - this.Location.X, this.CurrentFrameMouseState.Position.Y - this.Location.Y);
                isPressed = true;
            }
        }

        public void InitializeForms()
        {
            textLbl = new Label();
            textLbl.Text = "drag me!";
            textLbl.Size = new Vector2(50, 10);
            textLbl.AutoResize = true;
            textLbl.Location = new Vector2((this.Size.X / 2) - (textLbl.Size.X / 2), 10);
            textLbl.Alignment = TextAlignment.Center;
            textLbl.parent = this;
            Add(textLbl);
        }

        public override void Update(GameTime gameTime)
        {
            if(isPressed)
            {
                this.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);

                // react to parent matrix if any.
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                    isPressed = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
