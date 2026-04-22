namespace ServiceLocatorTask
{
    public interface IRepository { }

    public class Repository : IRepository
    {
    }

    public interface IServiceA
    {
        IRepository Repo { get; }
    }

    public class ServiceA : IServiceA
    {
        public IRepository Repo { get; }

        public ServiceA(IRepository repo)
        {
            Repo = repo;
        }
    }

    public interface ITransientService { }

    public class TransientService : ITransientService
    {
    }

    public interface ISingletonService { }

    public class SingletonService : ISingletonService
    {
    }

    public interface IMultiCtorService { }

    public class MultiCtorService : IMultiCtorService
    {
        public MultiCtorService() { }

        public MultiCtorService(IRepository repo) { }
    }
}