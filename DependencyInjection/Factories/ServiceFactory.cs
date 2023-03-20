namespace DependencyInjection.Factories
{
    public abstract class ServiceFactory : IServiceFactory, IDependencyInjectionController
    {
        public virtual IEnumerable<int> GetDependencies() => Array.Empty<int>();

        public abstract object? Invoke(IServiceProvider services);
    }
}