using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.GUI.Forms
{
    public class ButtonContainer<T> : Button
    {
        // Data to hold.
        private T data;

        // Public data to give out.
        public T Data { get { return data; } set { data = value; } }

        public ButtonContainer() : base()
        {
            data = default(T);
        }
    }
}
