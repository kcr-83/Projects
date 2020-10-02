using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteComponents
{
    public class SmallPizza : Pizza
    {
        public override double CalculateCost()
        {
            return 12.0;
        }

        public override string GetName()
        {
            return "Small Pizza";
        }
    }
}
