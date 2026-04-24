using ServiceLocatorTask.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask.Application
{
    public class MultiCtorService : IMultiCtorService
    {
        public MultiCtorService() { }

        public MultiCtorService(IRepository repo) { }
    }
}

