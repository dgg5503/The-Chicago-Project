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
    // Douglas Gliner
    class Button : Control
    {
        // Label on button
        private Label text;

        // Text on button
        //private string text;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text.Text; } set { text.Text = value; } }

        public Button()
        {
            text = new Label();
            text.Text = String.Empty;
            text.Size = new Vector2(1, 1); // doesnt matter?
            text.Location = new Vector2(0, 0);
            text.AutoResize = true;
            text.TextAlignment = TextAlignment.Center;
            text.parent = this;
            Add(text);
        }

        public override void Update(GameTime gameTime)
        {
            // Centered text, make this a bool or enum (?)
            /*
            if (text != null)
                text.Location = new Vector2(this.Size.X / 2 - text.Font.MeasureString(text.Text).X / 2, 0);
            */
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
