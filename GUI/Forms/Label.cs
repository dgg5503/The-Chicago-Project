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

    //Douglas Gliner
    class Label : Control
    {
        // Text on label
        private string text;

        // Autoresize to fit text?
        private bool autoResize;
        private Vector2 lastSize;

        // word wrap!
        private bool wordWrap;

        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text; } set { text = value; } }
        /// <summary>
        /// Autoresize the control to fit the text.
        /// </summary>
        public bool AutoResize { get { return autoResize; } set { autoResize = value; } }
        /// <summary>
        /// Enable or disable word wrap on this label, this only works if there is an attached parrent.
        /// </summary>
        public bool WordWrap { get { return wordWrap; } set { wordWrap = true; } }

        public Label()
        {
            text = String.Empty;

            // size will always be automatically resized!
            Size = Vector2.Zero;

            lastSize = Vector2.Zero;
            wordWrap = false;
            autoResize = true;
            Border = null;
            Fill = null;
        }

        public override void Update(GameTime gameTime)
        {
            // this is pretty meh and only applies to text
            // I should do this check in control not just here :P...... (?)

            this.Size = GetTextSize();

            if (this.Size == Vector2.Zero)
                this.Size = parent.Size;

            // WORD WRAP AGAIN HERE.

            if (this.Size != lastSize)
            {
                this.ControlSizeChange(lastSize);
                lastSize = this.Size;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            if (text == string.Empty)
                this.Size = this.parent.Size; // target size?
            else
                this.Size = GetTextSize();
            lastSize = this.Size;
        }

        private Vector2 GetTextSize()
        {
            #region word wrap
            // Thanks to https://gist.github.com/Sankra/5585584
            // Runar Ovesen Hjerpbakk
            if (wordWrap == true && parent != null && lastSize.X > parent.Size.X)
            {
                string[] words = text.Split(' ');
                StringBuilder wrappedText = new StringBuilder();
                float linewidth = 0f;
                float spaceWidth = Font.MeasureString(" ").X;
                for (int i = 0; i < words.Length; ++i)
                {
                    Vector2 size = Font.MeasureString(words[i]);
                    if (linewidth + size.X < parent.Size.X)
                    {
                        linewidth += size.X + spaceWidth;
                    }
                    else
                    {
                        wrappedText.Append("\n");
                        linewidth = size.X + spaceWidth;
                    }
                    wrappedText.Append(words[i]);
                    wrappedText.Append(" ");
                }

                text = wrappedText.ToString();
                
            }
            #endregion

            return Font.MeasureString(text);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
            
        }

    }
}
