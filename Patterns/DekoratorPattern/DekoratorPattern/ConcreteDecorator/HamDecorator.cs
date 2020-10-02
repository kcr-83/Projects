using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteDecorator
{
    public class HamDecorator : PizzaDecorator
    {
        public HamDecorator(Pizza pizza) : base(pizza)
        {
        }
        public override double CalculateCost()
        {
            return base.CalculateCost() + 4.15;
        }
        public override string GetName()
        {
            return base.GetName() + ", Ham";
        }
    }
}
