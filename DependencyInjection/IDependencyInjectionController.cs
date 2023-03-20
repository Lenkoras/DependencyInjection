namespace DependencyInjection
{
    /// <summary>
    /// Dependency injection controller. Using to detect recursive dependency.
    /// </summary>
    public interface IDependencyInjectionController
    {
        /// <summary>
        /// Returns which collection of hash codes this controller depends on.
        /// </summary>
        /// <returns>Collection of hash codes.</returns>
        IEnumerable<int> GetDependencies();
    }
}