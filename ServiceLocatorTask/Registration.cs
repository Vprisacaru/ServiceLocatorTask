using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLocatorTask
{
    public class Registration
    {
        public Type ImplementationType { get; set; } = null!;
        public Lifetime Lifetime { get; set; }
        public object? SingletonInstance { get; set; }
    }
}
