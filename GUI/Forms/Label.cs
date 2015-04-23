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

            // size will always be automatically resized!
            Size = Vector2.Zero;

            lastSize = Vector2.Zero;
            autoResize = true;
            Border = null;
            Fill = null;
        }

        public override void Update(GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            // IF SIZE CHANGED, REALLIGN

            this.Size = Font.MeasureString(text);

            if (this.Size == Vector2.Zero)
                this.Size = parent.Size;

            if (this.Size != lastSize)
            {
                this.ControlSizeChange(lastSize);
                Console.WriteLine("allign applied on {0}, old {1}, new {2}", text, lastSize, this.Size);
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
                this.Size = Font.MeasureString(text);
            lastSize = this.Size;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            

            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            spriteBatch.DrawString(Font, text, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
            
        }

    }
}
