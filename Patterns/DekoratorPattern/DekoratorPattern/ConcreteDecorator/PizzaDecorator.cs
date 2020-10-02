using System;
using System.Collections.Generic;
using System.Text;
using DekoratorPattern.Components;

namespace DekoratorPattern.ConcreteDecorator
{
    public class PizzaDecorator : Pizza
    {
        protected Pizza _pizza;
        public PizzaDecorator(Pizza pizza)
        {
            _pizza = pizza;
        }
        public override double CalculateCost()
        {
            return _pizza.CalculateCost();
        }

        public override string GetName()
        {
            return _pizza.GetName();
        }
    }
}
