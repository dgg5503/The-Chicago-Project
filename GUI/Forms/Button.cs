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
        // Text on button
        //private string text;
        private string text;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text; } set { text = value; } }

        public Button()
        {
            text = String.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
