using System.Reflection;

namespace DependencyInjection.Factories
{
    using Accessors;

    public sealed class ReflectionServiceFactory : ServiceFactory, IInitializable
    {
        private IDictionary<int, IServiceAccessor> serviceAccessorDictionary;
        private Type implementationType;
        private ReflectionServiceFactoryEngine? factoryEngine;
        private ReflectionServiceFactoryEngine FactoryEngine =>
            factoryEngine ??
                throw new InvalidOperationException(
                        $"The {nameof(IServiceFactory)} is not initialized. Use a {nameof(IInitializable.Initialize)} method to invoke factory initialization.");

        public ReflectionServiceFactory(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type implementationType)
        {
            ArgumentNullException.ThrowIfNull(serviceAccessorDictionary);
            ArgumentNullException.ThrowIfNull(implementationType);

            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new ArgumentException(nameof(implementationType), $"The {implementationType.Name} is invalid implementation type. Abstract classes and interfaces can not be created.");
            }

            this.serviceAccessorDictionary = serviceAccessorDictionary;
            this.implementationType = implementationType;
        }

        public override object Invoke(IServiceProvider services) =>
            FactoryEngine.Invoke(services);

        public void Initialize()
        {
            factoryEngine = new ReflectionServiceFactoryEngine(serviceAccessorDictionary, implementationType);
        }

        public override ISet<int> GetDependencies() =>
            GetHashSetDependencies(FactoryEngine.ConstructorInfo);

        private static HashSet<int> GetHashSetDependencies(ConstructorInfo ctor) => ctor.GetParameters().Select(ParameterTypeHashCode).ToHashSet();
        private static int ParameterTypeHashCode(ParameterInfo parameterInfo) => parameterInfo.ParameterType.GetHashCode();
    }
}