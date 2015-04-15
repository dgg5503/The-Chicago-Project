using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheChicagoProject.GUI.Forms
{
    public class DragableControl : Control
    {
        private Label textLbl;

        private Vector2 mouseOrigin;

        private bool isPressed;

        private DragableContainer[,] containerMatrix;

        string text;

        private int dragMatrixX;
        private int dragMatrixY;

        public int DragMatrixX { get { return dragMatrixX; } set { dragMatrixX = value; } }
        public int DragMatrixY { get { return dragMatrixY; } set { dragMatrixY = value; } }

        public DragableControl(string text, DragableContainer[,] containerMatrix)
        {
            // relative to container object
            this.Location = new Vector2(0, 0);
            this.Size = new Vector2(64, 64);

            this.containerMatrix = containerMatrix;
            this.Pressed += DragableControl_Pressed;
            this.text = text;


            isPressed = false;

            mouseOrigin = new Vector2(-1, -1);

            // Find first spot in cont
            
            dragMatrixX = -1;
            dragMatrixY = -1;

            InitializeForms();
        }

        void DragableControl_Pressed(object sender, EventArgs e)
        {
            if (containerMatrix != null && isPressed == false)
            {
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - this.Location.X, this.CurrentFrameMouseState.Position.Y - this.Location.Y);
                isPressed = true;
            }
        }

        public void InitializeForms()
        {
            textLbl = new Label();
            textLbl.Text = text;
            textLbl.Size = new Vector2(50, 10);
            textLbl.AutoResize = true;
            textLbl.Location = new Vector2((this.Size.X / 2) - (textLbl.Size.X / 2), 10);
            textLbl.Alignment = TextAlignment.Center;
            textLbl.parent = this;
            Add(textLbl);
        }

        public override void Update(GameTime gameTime)
        {
            if(containerMatrix != null && isPressed)
            {
                this.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);

                // If most of box is over a new container, snap to that box
                // else
                // return to last location.
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    for (int x = 0; x < containerMatrix.GetLength(0); x++)
                        for (int y = 0; y < containerMatrix.GetLength(1); y++ )
                        {
                            Vector2 globalLoc = containerMatrix[x, y].GlobalLocation();
                            Vector2 size = containerMatrix[x, y].Size;
                            if(new Rectangle((int)globalLoc.X, (int)globalLoc.Y, (int)Size.X, (int)Size.Y).Contains(new Vector2(CurrentFrameMouseState.Position.X,CurrentFrameMouseState.Position.Y)))
                            {
                                // nothing in that loc, place, else swap.
                                if(containerMatrix[x, y].CurrentControl == null)
                                {
                                    containerMatrix[x, y].CurrentControl = this;
                                    containerMatrix[dragMatrixX, dragMatrixY] = null;

                                    this.dragMatrixX = x;
                                    this.dragMatrixY = y;
                                }
                                else
                                {
                                    DragableControl tmp = containerMatrix[x, y].CurrentControl;

                                    containerMatrix[x, y].CurrentControl = this;
                                    containerMatrix[dragMatrixX, dragMatrixY].CurrentControl = tmp;

                                    containerMatrix[dragMatrixX, dragMatrixY].CurrentControl.DragMatrixX = dragMatrixX;
                                    containerMatrix[dragMatrixX, dragMatrixY].CurrentControl.DragMatrixY = dragMatrixY;

                                    this.dragMatrixX = x;
                                    this.dragMatrixY = y;
                                }
                                
                                 
                            }
                        }
                    isPressed = false;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
