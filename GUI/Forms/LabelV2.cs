using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

// Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{
    public class LabelV2 : Control
    {
        /// <summary>
        /// Holds a single line of text and that is all, NO WORD WRAP!!!!!!
        /// </summary>
        private class TextContainer : Control
        {
            // Text to hold.
            private string text;

            // Color to draw the text as.
            private Color color; 

            // Scale
            private float scale;

            /// <summary>
            /// Gets or sets the scale of this text container.
            /// </summary>
            public float Scale { get { return scale; } set { scale = value; Size *= scale; } }

            /// <summary>
            /// Get or set the string of text to hold for this single text container.
            /// </summary>
            public string Text { get { return text; } set { text = value; } }

            /// <summary>
            /// Get or set the color of the text.
            /// </summary>
            public Color Color { get { return color; } set { color = value; } }


            /// <summary>
            /// Creates a text container which simply holds a LINE OF TEXT and its size
            /// </summary>
            /// <param name="size">Size of this string.</param>
            /// <param name="text">Text of string.</param>
            /// <param name="color">Color of string</param>
            public TextContainer(Vector2 size, string text, Color color)
            {
                scale = 1;
                this.text = text;
                this.color = color;
                this.Size = size;
                this.Fill = null;
                this.Border = null;
            }

            public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
            {
                spriteBatch.DrawString(Font, text, this.GlobalLocation(), color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                base.Draw(spriteBatch, gameTime);
                
            }


        }
        // A single label will be a label in a container (a line of text in a container)
        // A label will take in text, seperate by new lines / word wrap by size of container and then set those new lines to new containers holding text.
        

        // option to contain all text (split lines by word wrap option,)
        // option to contain only the text that will fit with a certain scaling number. (word wrap only


        // option for word wrap type.

        // things to do before word wrap
        // - scaling
        // - decide on line spacing
        // - word wrap type (by spaces, by letters (add a dash at the end!)

        // if a line of text already has \n, ignore that in word wrap algo.

        // Fields
        // Line spacing
        private float lineSpacing;

        // Scaling
        private float scale;

        // Text
        private string text;

        // Color
        private Color color;

        // Global tmp text container
        private TextContainer textContainer;

        // Target size
        private Vector2 targetSize;

        // Legacy fields
        private string lastText;
        private Vector2 lastSize;

        // Bools


        /// <summary>
        /// Get or set the string of text to hold for this single text container.
        /// </summary>
        public string Text { get { return text; } set { if (Font != null) { UpdateTextControls(value); } else { text = value; } } }

        /// <summary>
        /// Get or set the color of the text.
        /// </summary>
        public Color Color { get { return color; } set { color = value; } }

        
        // LIST OF CONTROLS FOUND IN BASE
        public LabelV2(Vector2? targetSize = null)
        {
            // start with a single text container and wait for next actions after loadcontent.
            this.Fill = new FillInfo(Color.HotPink);
            //this.Border = null;
            this.color = Color.White;
            this.scale = 1;
            this.lineSpacing = .05f;

            // Target size for word wrapping.
            if (targetSize == null)
                this.Size = Vector2.One;
            else
            {
                this.Size = (Vector2)targetSize;
                this.targetSize = this.Size;
            }

        }

        protected override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            UpdateTextControls(text);

        }

        public void UpdateTextControls(string text)
        {
            Clear();

            // this shouldnt be here since the whole point of this label is to HAVE a target size, just for place holder tho.
            if (string.IsNullOrWhiteSpace(text))
                return;

            scale = 1;
            Vector2 measuredFontSize = Font.MeasureString(text);
            Vector2 measureWordWrappedSize = GetWordWrappedSize(text, 1);

            
            // Pre-check
                // if no target size given, set size to measurestring size.
            if (targetSize == Vector2.Zero)
            {
                this.Size = measuredFontSize;
                textContainer = new TextContainer(measuredFontSize, text, color);
                Add(textContainer);
            }
            else if (measureWordWrappedSize.Y > targetSize.Y)
            {
                // if you cant word wrap with the current scale. RESIZE!!

                this.Size = targetSize;
                textContainer = new TextContainer(measuredFontSize, text, color);
                Add(textContainer);
                /*
                scale = Math.Min(targetSize.X / measuredFontSize.X, targetSize.Y / measuredFontSize.Y);
                textContainer = new TextContainer(measuredFontSize, text, color);
                //textContainer.Scale = scale;
                Add(textContainer);*/
            }
            // Check 1
            // See if font.X > this.X
                // word wrap
            // Check 2
            // see if font.Y > this.Y
                // scale whole size by y.
            /*
            Vector2 fontSize = Font.MeasureString(text);
            if(fontSize.X > this.X)
            

            if (string.IsNullOrWhiteSpace(text))
                this.Size = this.parent.Size;
            else
                this.Size = GetTextSize();

            */
            this.ControlSizeChange(lastSize);
            lastSize = this.Size;
            lastText = text;
            alignApplied = true;
        }

        private Vector2 GetWordWrappedSize(string text, float scale)
        {
            /*
            if (string.IsNullOrWhiteSpace(text))
                return Vector2.Zero;
            */
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

            return Font.MeasureString(wrappedText.ToString());
        }

        private Vector2 GetTextSize()
        {
            #region word wrap
            // Thanks to https://gist.github.com/Sankra/5585584
            // Runar Ovesen Hjerpbakk
            if (Font.MeasureString(text).X > parent.Size.X && parent != null)
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


    }
}
