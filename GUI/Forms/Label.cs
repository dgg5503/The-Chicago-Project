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

    /*
     * TO-DO:
     * - Auto resize the control to fit the text size.
     * - 
     */

    class Label : Control
    {
        // Text on label
        private string text;

        // Autoresize to fit text?
        private bool autoResize;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text; } set { text = value; } }
        /// <summary>
        /// Autoresize the control to fit the text.
        /// </summary>
        public bool AutoResize { get { return autoResize; } set { autoResize = value; } }

        public Label()
        {
            text = String.Empty;
            autoResize = false;
        }

        public override void Update(GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            /*if(autoResize)
                this.Size = Font.MeasureString(text);*/

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            // does this actually do anything? (?)
            if(autoResize)
                this.Size = Font.MeasureString(text);

            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
