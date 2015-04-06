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

    enum TextAlignment
    {
        Left,
        Right,
        Center
    }
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

        // Alignment relative to parent
        private TextAlignment alignment;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text; } set { text = value; } }
        /// <summary>
        /// Autoresize the control to fit the text.
        /// </summary>
        public bool AutoResize { get { return autoResize; } set { autoResize = value; } }
        /// <summary>
        /// Sets the alignment of this text relative to its container (parent).
        /// </summary>
        public TextAlignment Alignment { get { return alignment; } set { alignment = value; } }

        public Label()
        {
            text = String.Empty;
            autoResize = false;
        }

        public override void Update(GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            if(autoResize)
                this.Size = Font.MeasureString(text);

            switch(alignment)
            {
                case TextAlignment.Center:
                    Location = new Vector2(parent.Size.X / 2 - Font.MeasureString(text).X / 2, 0);
                    break;

                case TextAlignment.Left:
                    Location = new Vector2(0, 0);
                    break;

                case TextAlignment.Right:
                    Location = new Vector2(parent.Size.X/ - Font.MeasureString(text).X, 0);
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            // does this actually do anything? (?)
            /*if(autoResize)
                this.Size = Font.MeasureString(text);*/

            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
