using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTwo
{
    public class DummyService2 : IDummyService2
    {
        public IEnumerable<string> GetDummyStrings()
        {
            return new List<string> { "dummy1", "dummy2" };
        }
    }
}
