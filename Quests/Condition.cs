using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Quests
{
    //class for converting strings to boolean operations
    class Condition
    {
        //fields
        private string conditionString;
        private string[] values;
        private string[] binaryTree;

        //properties
        public string ConditionString { 
            get { return conditionString; } 
            set { 
                conditionString = value; 
                values = conditionString.Split(' ');
                binaryTree = new string[values.Length];
            } }

        //constructor
        public Condition(string conditionString = "")
        {
            ConditionString = conditionString;
        }

        

    }
}
