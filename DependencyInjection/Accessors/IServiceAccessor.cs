namespace DependencyInjection.Accessors
{
    public interface IServiceAccessor
    {
        object? GetService(IServiceProvider services);
    }
}