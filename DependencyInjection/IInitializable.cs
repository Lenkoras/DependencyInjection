namespace DependencyInjection
{
    /// <summary>
    /// Signalizes a component is initializable and must be initialized before interacting.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>   
        /// Initializes a component and prepare it to work.
        /// </summary>
        void Initialize();
    }
}