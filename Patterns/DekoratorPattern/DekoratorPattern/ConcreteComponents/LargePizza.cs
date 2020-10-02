using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteComponents
{
    public class LargePizza : Pizza
    {
        public override double CalculateCost()
        {
            return 50.0;
        }

        public override string GetName()
        {
           return "Large Pizza";
        }
    }
}
