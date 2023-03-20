namespace DependencyInjection.Rules
{
    public interface IDependencyInjectionRule : IRule
    {
        /// <summary>
        /// Maximum allowable dependency injection depth.
        /// </summary>
        int MaxDependencyInjectionDepth { get; set; }
    }
}
