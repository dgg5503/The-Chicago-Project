using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    public static class Controls
    {

        public static Dictionary<string, Control> guiElements = new Dictionary<string, Control>()
        {
            {"mainMenu", new Menu()},
            {"pauseMenu", new PauseMenu()}
            //
        };
    }
}
