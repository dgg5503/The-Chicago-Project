using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.AI;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Quests
{
    class Mugging : Quest
    {
        public const string MUGGER_TEXTURE = "mugger.png";
        public const int MUGGER_WIDTH = 32;

        private LivingEntity mugger;
        private LowAI muggerAI;
        private Player player;

        //constructor
        public Mugging(string name, string objective, string description, Vector2 start, Player player) : base(name, objective, description, start, 0, 10)
        {
            this.player = player;
            mugger = new LivingEntity(new Rectangle((int)start.X, (int)start.Y, MUGGER_WIDTH, MUGGER_WIDTH), MUGGER_TEXTURE);
            muggerAI = new LowAI(mugger);
        }

        public override void Update()
        {
            if(mugger.health <= 0)
            {
                this.Completed(player);
            }
            else
            {
                throw new NotImplementedException("Needs to get the mugger to attack the player");
            }
        }

        public override void StartQuest()
        {
            if (mugger.health <= 0)
                base.Completed(this.player);
            base.StartQuest();
        }
    
    }
}
