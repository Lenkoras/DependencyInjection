using System.Reflection;

namespace DependencyInjection.Factories
{
    using Accessors;

    internal sealed class ReflectionServiceFactoryEngine
    {
        public ConstructorInfo ConstructorInfo { get; }

        private IServiceAccessor[] parameterServiceFactories;

        private bool IsNotEmpty => parameterServiceFactories.Length > 0;

        public ReflectionServiceFactoryEngine(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type implementationType)
        {
            ConstructorInfo = FindBestConstructor(serviceAccessorDictionary, implementationType);
            parameterServiceFactories = GetParameterServiceFactories(serviceAccessorDictionary, ConstructorInfo);
        }

        public object Invoke(IServiceProvider services) =>
            ConstructorInfo.Invoke(
                IsNotEmpty
                ?
                BuildParameters(services)
                :
                null
            );

        private object?[] BuildParameters(IServiceProvider services) =>
            parameterServiceFactories
                    .Select(serviceFactory => serviceFactory.GetService(services))
                    .ToArray();

        private static ConstructorInfo FindBestConstructor(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type implementationType)
        {
            ConstructorInfo[] constructors = implementationType.GetConstructors();
            if (constructors.Length < 1)
            {
                throw new InvalidOperationException($"The {implementationType.FullName} does not contains public constructors, so it's can not be resolved.");
            }
            else if (constructors.Length > 1) // if class contains only one (1) public constructor, then sort invocation is unnecessary
            {
                Array.Sort(constructors, CompareByParametersCountDescending); // sorts constructors by descending to start iteration from constructor with highest parameters count
            }
            var ctor = constructors.FirstOrDefault(ctor => ContainsParameters(serviceAccessorDictionary, ctor.GetParameters()));

            if (ctor == null) // constructor with resolved parameters not found
            {
                throw new InvalidOperationException($"Can not find suitable constructor. Same type in constructor of the {implementationType.Name} can not be resolved.");
            }

            return ctor;
        }

        private static int CompareByParametersCountDescending(ConstructorInfo a, ConstructorInfo b) =>
            b.GetParameters().Length.CompareTo(a.GetParameters().Length);

        private static bool ContainsParameters(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, ParameterInfo[] parameters)
        {
            if (parameters.Length < 1)
            {
                return true;
            }

            return parameters.Length == parameters.Count(parameter => serviceAccessorDictionary.ContainsKey(parameter.ParameterType.GetHashCode()));
        }

        private static IServiceAccessor[] GetParameterServiceFactories(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, ConstructorInfo constructorInfo) =>
            constructorInfo.GetParameters().Select(parameter => serviceAccessorDictionary[parameter.ParameterType.GetHashCode()]).ToArray();
    }
}