using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteComponents
{
    public class MediumPizza : Pizza
    {
        public override double CalculateCost()
        {
            return 25.0;
        }

        public override string GetName()
        {
           return "Medium Pizza";
        }
    }
}
