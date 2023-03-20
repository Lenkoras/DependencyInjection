namespace DependencyInjection.Factories
{
    public class FuncServiceFactory<TResult> : IServiceFactory
    {
        private readonly Func<IServiceProvider, TResult?> implementationFactory;

        public FuncServiceFactory(Func<IServiceProvider, TResult?> implementationFactory)
        {
            if (implementationFactory is null)
            {
                throw new ArgumentNullException(nameof(implementationFactory),
                    $"A {nameof(implementationFactory)} can not be null.");
            }
            this.implementationFactory = implementationFactory;
        }

        object? IServiceFactory.Invoke(IServiceProvider services) =>
            implementationFactory.Invoke(services);
    }
}