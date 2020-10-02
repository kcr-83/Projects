using DekoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.ConcreteDecorator
{
    public class LoggerPizzaDecorator : PizzaDecorator
    {
        public LoggerPizzaDecorator(Pizza pizza) : base(pizza)
        {
        }
        public override double CalculateCost()
        {
            Console.WriteLine("Before CalculateCost");
            var result = base.CalculateCost();
            Console.WriteLine("After CalculateCost " + result);
            return result;
        }
        public override string GetName()
        {
            Console.WriteLine("Before GetName");
            var name = base.GetName();
            Console.WriteLine("After GetName : " + name);
            return name;
        }
    }
}
