using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask
{
    public class MultiCtorService : IMultiCtorService
    {
        public MultiCtorService() { }

        public MultiCtorService(IRepository repo) { }
    }
}

