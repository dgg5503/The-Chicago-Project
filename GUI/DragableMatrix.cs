using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    // 
    class DragableMatrix : Control
    {
        // Holds all dragablecontrols
        private DragableControl[,] controlMatrix;

        private int seperatorWidth;

        public DragableMatrix(int xSize, int ySize, int seperatorWidth)
        {
            this.Size = new Vector2(xSize, ySize);

            // This will be relative to the inventory menu
            this.Location = new Vector2(100, 10);
            this.seperatorWidth = seperatorWidth;

            // init matrix
            /*
            for (int x = 0; x < xSize; x++)
                for (int y = 0; y < ySize; y++)
                    controlMatrix[x, y] = null;
             */
        }

        /// <summary>
        /// Adds a dragable control to the first available spot in the matrix.
        /// If it finds a spot, return the x,y position of the control otherwise, -1, -1.
        /// </summary>
        public int Add(DragableControl control)
        {
            for (int x = 0; x < controlMatrix.GetLength(0); x++)
                for (int y = 0; y < controlMatrix.GetLength(1); y++)
                    if (controlMatrix[x, y] == null)
                    {
                        controlMatrix[x, y] = control;
                        

                        control.DragMatrixX = x;
                        control.DragMatrixY = y;

                        control.Location = new Vector2(x * control.Size.X + seperatorWidth, y * control.Size.Y + seperatorWidth);

                        control.parent = this;
                        Add(control);
                        return 1;
                    }

            return -1;
        }

       

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = 0; x < controlMatrix.GetLength(0); x++)
                for (int y = 0; y < controlMatrix.GetLength(1); y++)
                    if (controlMatrix[x, y] != null)
                    {
                        controlMatrix[x, y].Draw(spriteBatch, gameTime);
                    }

            base.Draw(spriteBatch, gameTime);
        }




    }
}
