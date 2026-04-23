using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask
{
    public class ServiceA : IServiceA
    {
        public IRepository Repo { get; }

        public ServiceA(IRepository repo)
        {
            Repo = repo;
        }
    }

}
