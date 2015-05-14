using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Item;

//Douglas Gliner
namespace TheChicagoProject.GUI
{
    class DragableMatrixV2 : Control, IEnumerable<Item.Item>
    {
        // TO-DO:
        // - Add two context menus, one for weapon hovering one for holding

        // Matrix handling
        private List<DragableContainer> containers;
        private int maxSlots;
        private float sideLength;

        // Mouse handling
        private static Vector2 mouseOrigin;
        private static DragableControl currentDragableControl;
        private static DragableContainer hoveringDragableContaner;
        private static DragableContainer currentDragableContainer;

        // Context menu!
        private ItemStatsUI contextMenu;

        /// <summary>
        /// Get the item at this index.
        /// </summary>
        /// <param name="index">Index to get item.</param>
        /// <returns>The item or null if there is no item set.</returns>
        public Item.Item this[int index]
        {
            get
            {
                // get item at this index.
                if (containers[index].ControlContained == null)
                    return null;
                return containers[index].ControlContained.Item;
            }
        }

        /// <summary>
        /// Gets the calculated container side length.
        /// </summary>
        public float ContainerSideLength
        {
            get
            {
                return sideLength;
            }
        }

        public DragableMatrixV2(Vector2 size, int maxSlots)
        {
            // make sure valid w/h for number of max slots
            if (size.X * size.Y < maxSlots)
                throw new Exception("More max slots then allowed by size of control!");

            this.Size = size;
            
            containers = new List<DragableContainer>();
            this.maxSlots = maxSlots;

            // setup containers relative to loc.

            // if this controls list doesnt have the context menu, add it once!
            contextMenu = new ItemStatsUI(new Vector2(150, 150))
            {
                IsDrawn = false
            };

            SetupContainers();

            mouseOrigin = new Vector2(-1, -1);
            currentDragableControl = null;
        }

        /// <summary>
        /// Sets up containers relative to this matrix position. Will create square containers with sidelength appropriate for maxSlots.
        /// </summary>
        private void SetupContainers()
        {
            // TO-DO: center the controls or provide divider, they will probably be off a little due to floats.
            //Math.Max(Size.X % tileSize, Size.Y % tileSize);
            float dividerX = 0;
            float dividerY = 0;

            if (maxSlots == 1)
            {
                sideLength = Math.Min(Size.X, Size.Y);
            }
            else
            {

                // find optimal container size when max number of slots are in this matrix

                // http://stackoverflow.com/questions/868997/max-square-size-for-unknown-number-inside-rectangle
                // come up with an initial guess
                double aspect = (double)Size.Y / Size.X;
                double xf = Math.Sqrt(maxSlots / aspect);
                double yf = xf * aspect;
                int tx = (int)Math.Max(1.0, Math.Floor(xf));
                int ty = (int)Math.Max(1.0, Math.Floor(yf));
                int x_size = (int)Math.Floor((double)Size.X / tx);
                int y_size = (int)Math.Floor((double)Size.Y / ty);
                int tileSize = Math.Min(x_size, y_size);

                // test our guess:
                tx = (int)Math.Floor((double)Size.X / tileSize);
                ty = (int)Math.Floor((double)Size.Y / tileSize);
                if (tx * ty < maxSlots) // we guessed too high
                {
                    if (((tx + 1) * ty < maxSlots) && (tx * (ty + 1) < maxSlots))
                    {
                        // case 2: the upper bound is correct
                        //         compute the tileSize that will
                        //         result in (x+1)*(y+1) tiles
                        x_size = (int)Math.Floor((double)Size.X / (tx + 1));
                        y_size = (int)Math.Floor((double)Size.Y / (ty + 1));
                        tileSize = Math.Min(x_size, y_size);
                    }
                    else
                    {
                        // case 3: solve an equation to determine
                        //         the final x and y dimensions
                        //         and then compute the tileSize
                        //         that results in those dimensions
                        int test_x = (int)Math.Ceiling((double)maxSlots / ty);
                        int test_y = (int)Math.Ceiling((double)maxSlots / tx);
                        x_size = (int)Math.Min(Math.Floor((double)Size.X / test_x), Math.Floor((double)Size.Y / ty));
                        y_size = (int)Math.Min(Math.Floor((double)Size.X / tx), Math.Floor((double)Size.Y / test_y));
                        tileSize = Math.Max(x_size, y_size);
                    }
                }

                sideLength = tileSize;

                dividerX = (Size.X % tileSize) / ((float)Math.Floor(Size.X / tileSize) - 1);
                dividerY = (Size.Y % tileSize) / ((float)Math.Floor(Size.Y / tileSize) - 1);
            }

            // add the containers via for
            int count = 0;
            for (float y = 0; y + sideLength <= Size.Y && count < maxSlots; y += (sideLength + dividerY))
                for (float x = 0; x + sideLength <= Size.X && count < maxSlots; x += (sideLength + dividerX))
                {
                    DragableContainer tmpContainer = new DragableContainer(new Vector2(sideLength, sideLength));
                    tmpContainer.Location = new Vector2(x, y);
                    tmpContainer.HoverRelease += tmpContainer_HoverRelease;
                    tmpContainer.Hover += tmpContainer_Hover;
                    tmpContainer.Pressed += tmpContainer_Pressed;
                    tmpContainer.parent = this;
                    Add(tmpContainer);
                    containers.Add(tmpContainer);
                    count++;
                }
            
        }

        void tmpContainer_Hover(object sender, EventArgs e)
        {
            
            // if this container does not have an item in it, return
            DragableContainer container = sender as DragableContainer;

            if (container == null || container.ControlContained == null || container.ControlContained.Item == null)
            {
                //Console.WriteLine("nothing");
                //contextMenu.IsDrawn = false;
                return;
            }

            // display hovering container showing stats of item.
            Item.Item item = container.ControlContained.Item;

            // menu location
            if (currentDragableContainer == null)
                contextMenu.Location = new Vector2(this.CurrentFrameMouseState.Position.X, this.CurrentFrameMouseState.Position.Y);
            else
                contextMenu.Location = new Vector2(currentDragableControl.GlobalLocation().X + currentDragableControl.Size.X, currentDragableControl.GlobalLocation().Y);



            // load item info
            if (contextMenu.Item != item)
                contextMenu.Load(item);
                
            // make sure it draws
            contextMenu.IsDrawn = true;
        }

        void tmpContainer_Pressed(object sender, EventArgs e)
        {
            // Press a container
            // If the container has a control in it, grab it
            // DONT Clear that control from the container
            DragableContainer clickedOn = sender as DragableContainer;

            contextMenu.IsDrawn = false;

            if (currentDragableControl == null && clickedOn.ControlContained != null)
            {
                

                if (currentDragableContainer == null)
                    currentDragableContainer = clickedOn;

                currentDragableControl = clickedOn.ControlContained;
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - currentDragableControl.Location.X, this.CurrentFrameMouseState.Position.Y - currentDragableControl.Location.Y);
                
            }
        }

        void tmpContainer_HoverRelease(object sender, EventArgs e)
        {
            // Check to see if the container released over has a control in it
                // if so, swap
                // else add.
            if (currentDragableControl != null)
            {
                DragableContainer hoveringOver = sender as DragableContainer;
                hoveringDragableContaner = hoveringOver;

                DragableControl lastControl = hoveringOver.SetDragableControl(currentDragableControl);

                if (lastControl != null && currentDragableContainer != null)
                    currentDragableContainer.SetDragableControl(lastControl);

                currentDragableContainer = null;
                currentDragableControl = null;
            }
        }
        
        /// <summary>
        /// Adds a control to the matrix. Will be added to the first empty container.
        /// </summary>
        /// <param name="control">The control to add.</param>
        public void AddToMatrix(Item.Item item)
        {
            // find first open container and set.
            foreach(DragableContainer container in containers)
            {
                if(container.ControlContained == null)
                {
                    DragableControl tmpDragable = new DragableControl(item, sideLength);
                    container.SetDragableControl(tmpDragable);
                    break;
                }
            }

            // TO-DO: prompt to drop something.
        }

        public override void Update(GameTime gameTime)
        {
            if (currentDragableControl != null)
            {
                currentDragableControl.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    // if hovering over no slot, return the control back to original location (only if the control is inside the root matrix window)
                    if (hoveringDragableContaner == null && this.RootParent.GlobalRectangle.Contains(this.CurrentFrameMouseState.Position)) // && inside the window.
                    {
                        currentDragableControl.Location = Vector2.Zero;
                        
                        currentDragableControl = null;
                        currentDragableContainer = null;
                    }
                    else
                    {
                        // TO-DO: prompt to drop the item from inventory.
                        currentDragableControl.Location = Vector2.Zero;
                        currentDragableControl = null;
                        currentDragableContainer = null;
                    }

                }
            }
            
            // updated every frame so this is fine.
            hoveringDragableContaner = null;
            
            contextMenu.Update(gameTime);
            
            base.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);

            base.Draw(spriteBatch, gameTime);

            // hack for overlap drawing.
            if (currentDragableControl != null)
                currentDragableControl.Draw(spriteBatch, gameTime);

            // hack for context menu
            if (contextMenu != null)
                contextMenu.Draw(spriteBatch, gameTime);

            contextMenu.IsDrawn = false;
        }

        protected override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            contextMenu.LoadVisuals(contentManager);
            base.LoadContent(contentManager);
        }

        protected override void LoadTextures(GraphicsDevice graphics)
        {
            contextMenu.LoadVisuals(null, graphics);
            base.LoadTextures(graphics);
        }

        public override void Clear()
        {
            foreach (DragableContainer container in containers)
                container.Clear();
        }

        public IEnumerator<Item.Item> GetEnumerator()
        {
            for (int i = 0; i < containers.Count; i++)
            {
                // YIELLLLLLD (i think this returns the items in the same order as given.)
                if (containers[i].ControlContained.Item != null)
                    yield return containers[i].ControlContained.Item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
