using Castle.Windsor;
using Castle.MicroKernel.Registration;
using DekoratorPattern.ConcreteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using DekoratorPattern.Components;

namespace DekoratorPattern.ConcreteDecorator
{
    public static class BootStraper
    {
        public static IWindsorContainer Container; 

        public static void Boot()
        {
            Container = new WindsorContainer();

            Container.Register(
                Component.For<Pizza>().ImplementedBy<LoggerPizzaDecorator>(),
                Component.For<Pizza>().ImplementedBy<HamDecorator>(),
                Component.For<Pizza>().ImplementedBy<ChampignonsDecorator>(),
                Component.For<Pizza>().ImplementedBy<CheeseDecorator>(),
                Component.For<Pizza>().ImplementedBy<LargePizza>()
                );

            Pizza p = Container.Resolve<Pizza>();
            var name = p.GetName();
        }
    }
}
