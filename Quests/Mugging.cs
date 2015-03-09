using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.AI;
using TheChicagoProject.Entity;

namespace TheChicagoProject.Quests
{
    class Mugging
    {
        public const string MUGGER_TEXTURE = "mugger.png";

        private LivingEntity mugger;
        private LowAI muggerAI;
        private Quest quest;


        //constructor
        public Mugging(Quest quest)
        {
            LivingEntity = new LivingEntity(quest.StartPoint, MUGGER_TEXTURE);
        }
    
    }
}
