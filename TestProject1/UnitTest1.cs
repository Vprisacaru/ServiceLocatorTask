using System;
using Xunit;

namespace ServiceLocatorTask
{
    public class UnitTest1
    {
        [Fact]
        public void Transient_GetTwice_ReturnsDifferentObjects()
        {
            var locator = new ServiceLocator();
            locator.Register<ITransientService, TransientService>(Lifetime.Transient);

            var first = locator.Get<ITransientService>();
            var second = locator.Get<ITransientService>();

            Assert.NotSame(first, second);
        }

        [Fact]
        public void Singleton_GetTwice_ReturnsSameObject()
        {
            var locator = new ServiceLocator();
            locator.Register<ISingletonService, SingletonService>(Lifetime.Singleton);

            var first = locator.Get<ISingletonService>();
            var second = locator.Get<ISingletonService>();

            Assert.Same(first, second);
        }

        [Fact]
        public void Service_WithDependency_IsResolvedCorrectly()
        {
            var locator = new ServiceLocator();
            locator.Register<IRepository, Repository>(Lifetime.Singleton);
            locator.Register<IServiceA, ServiceA>(Lifetime.Transient);

            var service = locator.Get<IServiceA>();

            Assert.NotNull(service);
            Assert.NotNull(service.Repo);
            Assert.IsType<Repository>(service.Repo);
        }

        [Fact]
        public void Registering_SameServiceTwice_Throws()
        {
            var locator = new ServiceLocator();
            locator.Register<IRepository, Repository>(Lifetime.Singleton);

            Assert.Throws<InvalidOperationException>(() =>
                locator.Register<IRepository, Repository>(Lifetime.Transient));
        }

        [Fact]
        public void Registering_ServiceWithMultipleConstructors_Throws()
        {
            var locator = new ServiceLocator();

            Assert.Throws<InvalidOperationException>(() =>
                locator.Register<IMultiCtorService, MultiCtorService>(Lifetime.Transient));
        }

        [Fact]
        public void Getting_UnregisteredService_Throws()
        {
            var locator = new ServiceLocator();

            Assert.Throws<InvalidOperationException>(() => locator.Get<IRepository>());
        }
    }
}