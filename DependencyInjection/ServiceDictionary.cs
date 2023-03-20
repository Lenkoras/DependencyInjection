namespace DependencyInjection.Extensions
{
    using Accessors;
    using Rules;

    public class ServiceDictionary : Dictionary<int, IServiceAccessor>
    {
        public ServiceDictionary()
        {
            RuleCollection = new();
        }

        public List<IRule> RuleCollection { get; }

        public IServiceProvider BuildServiceProvider()
        {
            return this.BuildServiceProvider(RuleCollection);
        }
    }
}