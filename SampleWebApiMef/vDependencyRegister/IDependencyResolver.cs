using System;
using System.Collections.Generic;
using System.Text;

namespace vDependencyResolver
{
    public interface IDependencyResolver
    {
        void SetUp(IDependencyRegister dependencyRegister);

    }
}
