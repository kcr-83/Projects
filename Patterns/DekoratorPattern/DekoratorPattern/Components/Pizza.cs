using System;
using System.Collections.Generic;
using System.Text;

namespace DekoratorPattern.Components
{
    public abstract class Pizza
    {
        public abstract double CalculateCost();

        public abstract string GetName();
    }
}
