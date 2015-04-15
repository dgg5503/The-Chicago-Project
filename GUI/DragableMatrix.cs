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
        // Matrix handling
        private DragableControl[,] controlMatrix;
        private int count;

        // Mouse handling
        private Vector2 mouseOrigin;

        private bool isPressed;



        private int seperatorWidth;

        public DragableMatrix(int xSize, int ySize, int seperatorWidth)
        {
            controlMatrix = new DragableControl[xSize, ySize];

            count = 0;

            mouseOrigin = new Vector2(-1, -1);
            isPressed = false;

            this.Size = new Vector2((DragableControl.SIDE_LENGTH + seperatorWidth) * xSize, (DragableControl.SIDE_LENGTH + seperatorWidth) * ySize);
            this.Location = new Vector2(0, 0);
            this.seperatorWidth = seperatorWidth;
        }

        /// <summary>
        /// Adds a control to the first available spot in the matrix, if none available, an exception is thrown.
        /// </summary>
        /// <param name="control">DragableControl to add</param>
        public void AddToMatrix(DragableControl control)
        {
            if(count >= controlMatrix.GetLength(0) * controlMatrix.GetLength(1))
                throw new Exception("GUI/Inventory dimension mismatch: too many added!");


            for (int y = 0; y < controlMatrix.GetLength(1); y++)
                for (int x = 0; x < controlMatrix.GetLength(0); x++)
                    if (controlMatrix[x, y] == null)
                    {
                        controlMatrix[x, y] = control;

                        control.Location = new Vector2(x * control.Size.X + seperatorWidth, y * control.Size.Y + seperatorWidth);

                        control.Pressed += control_Pressed;

                        control.parent = this;

                        Add(control);

                        count++;
                        return;
                    }

            throw new Exception("GUI/Inventory dimension mismatch.");
        }


        void control_Pressed(object sender, EventArgs e)
        {
            DragableControl clickedOn = sender as DragableControl;

            if(clickedOn != null)
                clickedOn.Clear();
        }
       
       

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }




    }
}
