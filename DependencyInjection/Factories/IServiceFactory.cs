namespace DependencyInjection.Factories
{
    public interface IServiceFactory
    {
        object? Invoke(IServiceProvider services);
    }
}