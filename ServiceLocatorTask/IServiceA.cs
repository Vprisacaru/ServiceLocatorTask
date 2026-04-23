using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask
{
    public interface IServiceA
    {
        IRepository Repo { get; }
    }
}
