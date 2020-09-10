using ServiceContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using vDependencyResolver;

namespace NewService
{
    [Export(typeof(IDependencyResolver))]
    public class ServiceDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<IDummyService1, NewService>();
        }
    }
}
