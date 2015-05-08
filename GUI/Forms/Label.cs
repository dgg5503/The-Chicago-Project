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
        private string lastText;

        // Autoresize to fit text?
        private bool autoResize;
        private float scale;
        private Color color;
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
        public bool WordWrap { get { return wordWrap; } set { wordWrap = value; } }
        /// <summary>
        /// Gets or sets the scale or size of this labels text.
        /// </summary>
        public float Scale { get { return scale; } set { if (scale <= 0) scale = 1; else scale = value; } }
        /// <summary>
        /// Get or set the color of the text.
        /// </summary>
        public Color Color { get { return color; } set { color = value; } }

        public Label()
        {
            text = String.Empty;
            scale = 1;
            color = Color.White;

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
            

            if (this.Size == Vector2.Zero) // or string.isnull?
                this.Size = parent.Size;

            if (this.Text != lastText)
            {
                this.Size = GetTextSize(); // scaling should apply to text wrap too...
                //text = TextAlign();
                this.ControlSizeChange(lastSize);
                lastSize = this.Size;
                lastText = text;
                alignApplied = true;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            if (string.IsNullOrWhiteSpace(text))
                this.Size = this.parent.Size; // target size?
            else
                this.Size = GetTextSize();
            
            lastSize = this.Size;
            lastText = text;
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
                float spaceWidth = (Font.MeasureString(" ") * scale).X;
                for (int i = 0; i < words.Length; ++i)
                {
                    Vector2 size = Font.MeasureString(words[i]) * scale;
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
            return Font.MeasureString(text) * scale;
        }

        public string TextAlign()
        {
            // get longest line of text AFTER word wrap and scaling.
            // use that line as reference for all other text.
                // move over other text by doing longest.X / 2 - tmpWord.X / 2
            string[] sentances = text.Split('\n');
            if (sentances.Length == 1)
                return text;

            // find longest line.
            float longestLine = 0;
            foreach (String s in sentances)
                if (Font.MeasureString(s).X > longestLine)
                    longestLine = Font.MeasureString(s).X;

            // foreach through all text lines and center via spaces.
            StringBuilder sb = new StringBuilder();
            float spaceWidth = (Font.MeasureString(" ")).X;
            foreach(String s in sentances)
            {
                float distanceFromCenter = longestLine / 2 - Font.MeasureString(s).X / 2;
                int numOfSpaces = (int)distanceFromCenter / (int)spaceWidth;
                string centeredText = "";
                for (int i = 0; i < numOfSpaces; i++)
                    centeredText += ' ';
                centeredText += s;
                sb.Append(centeredText + "\n");
            }

            return sb.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            spriteBatch.DrawString(Font, text, this.GlobalLocation(), color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            base.Draw(spriteBatch, gameTime);
            
            
        }

    }
}
