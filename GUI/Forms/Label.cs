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
    class Label : Control
    {
        // Text on label
        private string text;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text; } set { text = value; } }

        public Label()
        {
            text = String.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
