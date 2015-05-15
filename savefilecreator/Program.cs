using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace savefilecreator
{
    class Program
    {
        static void Main(string[] args)
        {
            //an easy way to create save files
            using (Stream fileStream = File.OpenWrite("default.save"))
            using (BinaryWriter output = new BinaryWriter(fileStream))
            {
#if true
                /*Save Files*/
                /*world*/           output.Write("main");
                /*StartX*/          output.Write(2470);
                /*StartY*/          output.Write(1856);
                /*MaxHealth*/       output.Write(10);
                /*Cash*/            output.Write(40);
                /*QPoints*/         output.Write(0);
                /*num quests*/      output.Write(4);
                /*Quest*/           output.Write("Crazed Gunman");
                /*Status*/          output.Write(1);
                /*Quest*/           output.Write("Gang War");
                /*Status*/          output.Write(1);
                /*Quest*/           output.Write("Mugging");
                /*Status*/          output.Write(1);
                /*Quest*/           output.Write("Sniper");
                /*Status*/          output.Write(1);
                /*Num items*/       output.Write(10);
                /*ItemName*/        output.Write("Gun");
                /*ItemName*/        output.Write("Knife");
                /*ItemName*/        output.Write("The Screwdriver");
                /*ItemName*/        output.Write("Browning");
                /*ItemName*/        output.Write("M1911");
                /*ItemName*/        output.Write("Model 10");
                /*ItemName*/        output.Write("Zag");
                /*ItemName*/        output.Write("Tommy");
                /*ItemName*/        output.Write("Uzi");
                /*ItemName*/        output.Write("w");
                /*ActiveWeapon*/    output.Write(0);
#endif
#if false
                /*Weapon Files*/
                /*Type*/            output.Write("Weapon");
                /*Rate Of Fire*/    output.Write(400);
                /*Damage*/          output.Write(1);
                /*Reload Time*/     output.Write((double)3);
                /*Max Clip*/        output.Write(30);
                /*Spread*/          output.Write((double)100);
                /*Loaded Ammo*/     output.Write(30);
                /*Ammo*/            output.Write(120);
                /*Name*/            output.Write("W");
                /*Texture Key*/     output.Write("basic_gun_preview");
#endif
                output.Close();
            }
        }
    }
}
