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
    /// <summary>
    /// A place holder for a dragable control. These can be placed anywhere but must be assigned to a specific dictionary!
    /// </summary>
    class DragableContainer : Control
    {
        // Fields
        // Name of control
        private string name;

        // ID information
        private int myID;
        private static int id = 0;

        // Dragable control to hold in this container
        private DragableControl dragableControl;

        // Properties
        /// <summary>
        /// Returns name of the dragable container.
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Returns the ID of this dragable container.
        /// </summary>
        public int ID { get { return myID; } }
        /// <summary>
        /// Gets or sets the control contained in this dragbale container.
        /// </summary>
        public DragableControl ControlContained
        { 
            get 
            { 
                return dragableControl; 
            } 
            set 
            { 
                dragableControl = value; 
            } 
        }

        public DragableContainer(Vector2 size, DragableControl dragableControl = null, string name = null)
        {
            this.Size = size;

            this.name = name;
            this.dragableControl = dragableControl;

            myID = id++;
        }


    }
}
