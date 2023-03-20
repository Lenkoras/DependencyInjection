namespace DependencyInjection.Rules
{
    public interface IRule
    {
        /// <summary>
        /// Ensures compliance with the rule. If <see cref="IsPasses"/> will return <see langword="false"/>, then an exception should be thrown. 
        /// </summary>
        void EnsureCompliance();

        /// <summary>
        /// Returns whether the rules are being observed.
        /// </summary>
        bool IsPasses { get; }
    }
}