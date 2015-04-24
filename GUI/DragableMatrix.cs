using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    //Douglas Gliner
    class DragableMatrix : Control
    {
        // Matrix handling
        //private DragableControl[,] controlMatrix;
        private Dictionary<DragableControl, Vector2> controlDictionary;
        private int xSize;
        private int ySize;
        private int xCount;
        private int yCount;

        // Mouse handling
        private Vector2 mouseOrigin;
        private DragableControl currentDragableControl;
        private DragableControl hoveringDragableControl;

        private int seperatorWidth;

        public DragableMatrix(int xSize, int ySize, int seperatorWidth)
        {
            //controlMatrix = new DragableControl[xSize, ySize];
            controlDictionary = new Dictionary<DragableControl, Vector2>();
            this.xSize = xSize;
            this.ySize = ySize;
            xCount = 0;
            yCount = 0;

            mouseOrigin = new Vector2(-1, -1);
            currentDragableControl = null;

            this.Size = new Vector2((DragableControl.SIDE_LENGTH) * xSize, (DragableControl.SIDE_LENGTH) * ySize);
            this.seperatorWidth = seperatorWidth;
        }

        /*
        /// <summary>
        /// Adds a control to the first available spot in the matrix, if none available, an exception is thrown.
        /// </summary>
        /// <param name="control">DragableControl to add</param>
        public void AddToMatrix(DragableControl control)
        {
            if (controlDictionary.Count >= controlMatrix.GetLength(0) * controlMatrix.GetLength(1))
                throw new Exception("GUI/Inventory dimension mismatch: too many added!");


            for (int y = 0; y < controlMatrix.GetLength(1); y++)
                for (int x = 0; x < controlMatrix.GetLength(0); x++)
                    if (controlMatrix[x, y] == null)
                    {
                        controlMatrix[x, y] = control;

                        control.Location = new Vector2((x * control.Size.X), (y * control.Size.Y));

                        control.Pressed += control_Pressed;

                        control.Hover += control_Hover;

                        control.parent = this;

                        controlDictionary.Add(control, control.Location);

                        Add(control);
     
                        return;
                    }
            

            throw new Exception("GUI/Inventory dimension mismatch.");
        }
        */
        /// <summary>
        /// Adds a control to the first available spot in the matrix, if none available, an exception is thrown.
        /// </summary>
        /// <param name="control">DragableControl to add</param>
        public void AddToMatrix(DragableControl control)
        {
            if(xCount < xSize && yCount < ySize)
            {
                control.Location = new Vector2((xCount * control.Size.X), (yCount * control.Size.Y));

                control.Pressed += control_Pressed;

                control.Hover += control_Hover;

                control.parent = this;

                controlDictionary.Add(control, control.Location);
                
                Add(control);

                if (xCount + 1 < xSize)
                    xCount++;
                else
                    if (yCount + 1 < ySize)
                    {
                        yCount++;
                        xCount = 0;
                    }
            }
            else
            {
                throw new Exception("GUI/Inventory dimension mismatch: too many added!");
            }

            
        }

        /// <summary>
        /// Out a dragable control outside of the matrix bounds.
        /// 
        /// HACKY HACK :(
        /// </summary>
        /// <param name="control">A REFERENCE to the control</param>
        /// <param name="offset">The offset to top left of this matrix.</param>
        public void AddOutsideMatrix(DragableControl control, Vector2 offset)
        {
            control.Location = this.Location + offset;

            control.Pressed += control_Pressed;

            control.Hover += control_Hover;

            control.parent = this;

            controlDictionary.Add(control, control.Location);

            Add(control);
        }

        void control_Hover(object sender, EventArgs e)
        {
            // We only care about what object we're hovering over if we have an object in our hand
            if (currentDragableControl != null)
            {
                DragableControl hoveringOver = sender as DragableControl;

                if (hoveringOver != null && hoveringOver != currentDragableControl)
                    hoveringDragableControl = hoveringOver;
            }
        }


        private void control_Pressed(object sender, EventArgs e)
        {
            DragableControl clickedOn = sender as DragableControl;

            if (currentDragableControl == null && clickedOn != null)
            {
                currentDragableControl = clickedOn;
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - currentDragableControl.Location.X, this.CurrentFrameMouseState.Position.Y - currentDragableControl.Location.Y);
            }
        }


        public override void Update(GameTime gameTime)
        {
            if (controlDictionary != null && currentDragableControl != null)
            {
                currentDragableControl.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    // if hovering over no slot, return the control back to original location. (COULD BE A BLANK SPACE (?))
                    if (hoveringDragableControl == null)
                    {
                        currentDragableControl.Location = controlDictionary[currentDragableControl];
                        currentDragableControl = null;
                    }
                    else
                    {
                        // else swap spaces
                        // changes spaces in dic AND matrix.
                        //Vector2 controlToReplace = LocationToGrid(hoveringDragableControl.Location);
                        //Vector2 controlInHand = LocationToGrid(currentDragableControl.Location);

                        Vector2 tmpLoc = controlDictionary[currentDragableControl];
                        currentDragableControl.Location = controlDictionary[hoveringDragableControl];
                        hoveringDragableControl.Location = tmpLoc;

                        controlDictionary[currentDragableControl] = currentDragableControl.Location;
                        controlDictionary[hoveringDragableControl] = hoveringDragableControl.Location;

                        //DragableControl tmp = controlMatrix[(int)controlInHand.X, (int)controlInHand.Y];
                        //controlMatrix[(int)controlInHand.X, (int)controlInHand.Y] = controlMatrix[(int)controlToReplace.X, (int)controlToReplace.Y];
                        //controlMatrix[(int)controlToReplace.X, (int)controlToReplace.Y] = tmp;

                        currentDragableControl = null;
                    }
                 
                }
            }

            // updated every frame so this is fine.
            hoveringDragableControl = null;

            base.Update(gameTime); 
        }
       
        public Item.Item GetItemFromLocation(Vector2 location)
        {
            foreach (KeyValuePair<DragableControl, Vector2> kvp in controlDictionary)
                if (kvp.Value == location)
                    return kvp.Key.Item;
            return null;
        }

        private Vector2 LocationToGrid(Vector2 location)
        {
            return new Vector2(location.X / (DragableControl.SIDE_LENGTH + seperatorWidth), location.Y / (DragableControl.SIDE_LENGTH + seperatorWidth));
        }

        public override void Clear()
        {
            //controlMatrix.Initialize();
            xCount = 0;
            yCount = 0;
            controlDictionary.Clear();
            base.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);

            base.Draw(spriteBatch, gameTime);

            // hack for overlap drawing.
            if (currentDragableControl != null)
                currentDragableControl.Draw(spriteBatch, gameTime);
        }




    }
}
