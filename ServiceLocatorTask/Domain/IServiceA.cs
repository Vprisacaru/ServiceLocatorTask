using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask.Interface
{
    public interface IServiceA
    {
        IRepository Repo { get; }
    }
}
