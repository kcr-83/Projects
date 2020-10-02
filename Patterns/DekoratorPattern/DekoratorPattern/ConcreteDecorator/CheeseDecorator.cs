using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteDecorator
{
    public class CheeseDecorator : PizzaDecorator
    {
        public CheeseDecorator(Pizza pizza) : base(pizza)
        {
        }
        public override double CalculateCost()
        {
            return base.CalculateCost() + 2.5;
        }
        public override string GetName()
        {
            return base.GetName() + ", Cheese";
        }
    }
}
