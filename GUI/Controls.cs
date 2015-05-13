using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI.Forms;

// Douglas Gliner
namespace TheChicagoProject.GUI
{
    public static class Controls
    {

        public static Dictionary<string, Control> guiElements = new Dictionary<string, Control>()
        {
            {"mainMenu", new Menu()},
            {"pauseMenu", new PauseMenu()},
            {"inventoryMenu", new InventoryMenu()},
            {"weaponInfoUI", new WeaponInfoUI()},
            {"livingEntityInfoUI", new LivingEntityInfoUI()},
            {"weaponWheel", new WeaponWheelUI()},
            {"questLog", new QuestLogUI()}
        };
    }
}
