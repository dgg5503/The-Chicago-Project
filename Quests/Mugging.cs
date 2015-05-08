using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.AI;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;
using TheChicagoProject.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheChicagoProject.Quests
{
    //Sean Levorse
    class Mugging : Quest
    {
        /*DESCR*/

        //public const string MUGGER_TEXTURE = "mugger.png";
        public const int MUGGER_WIDTH = 32;

        private LivingEntity mugger;
        private LowAI muggerAI;
        private Weapon knife;


        //constructor
        public Mugging(string name, string objective, string description, Vector2 start, Player player, WorldManager manager) : base(name, objective, description, start, player, manager, WinCondition.EnemyDies, 0, 10)
        {
            
            mugger = new LivingEntity(new FloatRectangle(start.X, start.Y, MUGGER_WIDTH, MUGGER_WIDTH), Sprites.spritesDictionary["mugger"], 10);
            knife = new Weapon(20, 1, 3, "Knife", 1, 99);
            muggerAI = new LowAI(mugger);
            mugger.ai = muggerAI;
            entitites = new List<Entity.Entity>();
            entitites.Add(mugger);
            EnemyToKill = mugger;
            //manager.CurrentWorld.manager.AddEntity(mugger);
        }
        /*
        public override void Update()
        {
            if(mugger.health <= 0)
            {
                this.Completed(player);
                
            }
            else
            {
                mugger.Attack(0, knife);
            }
        }
        */
        public override void StartQuest()
        {
            if (mugger.health <= 0)
                base.Completed();
            base.StartQuest();
        }
    
    }
}
