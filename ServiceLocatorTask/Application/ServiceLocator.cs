using ServiceLocatorTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceLocatorTask.Application
{
   
    public class ServiceLocator
    {
        
        private readonly Dictionary<Type, Registration> _services = new();

        public void Register<TInterface, TImplementation>(Lifetime lifetime)
            where TImplementation : class, TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type implementationType = typeof(TImplementation);

            if (_services.ContainsKey(interfaceType))
                throw new InvalidOperationException(
                    $"Service '{interfaceType.Name}' is already registered.");

            ConstructorInfo[] publicCtors = implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (publicCtors.Length != 1)
                throw new InvalidOperationException(
                    $"Service '{implementationType.Name}' must have exactly one public constructor.");

            _services[interfaceType] = new Registration
            {
                ImplementationType = implementationType,
                Lifetime = lifetime,
                SingletonInstance = null
            };
        }

        public T Get<T>()
        {
            return (T)Resolve(typeof(T), new HashSet<Type>());
        }

        private object Resolve(Type interfaceType, HashSet<Type> resolvingStack)
        {
            if (!_services.TryGetValue(interfaceType, out Registration? registration))
                throw new InvalidOperationException(
                    $"Service '{interfaceType.Name}' is not registered.");

            if (registration.Lifetime == Lifetime.Singleton && registration.SingletonInstance != null)
                return registration.SingletonInstance;

            if (!resolvingStack.Add(interfaceType))
                throw new InvalidOperationException(
                    $"Circular dependency detected while resolving '{interfaceType.Name}'.");

            try
            {
                object instance = CreateInstance(registration.ImplementationType, resolvingStack);

                if (registration.Lifetime == Lifetime.Singleton)
                    registration.SingletonInstance = instance;

                return instance;
            }
            finally
            {
                resolvingStack.Remove(interfaceType);
            }
        }

        private object CreateInstance(Type implementationType, HashSet<Type> resolvingStack)
        {
            ConstructorInfo ctor = implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            ParameterInfo[] parameters = ctor.GetParameters();

            if (parameters.Length == 0)
                return Activator.CreateInstance(implementationType)!;

            object[] args = parameters
                .Select(p => Resolve(p.ParameterType, resolvingStack))
                .ToArray();

            return ctor.Invoke(args);
        }
    }
}