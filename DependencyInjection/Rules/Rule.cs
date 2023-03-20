namespace DependencyInjection.Rules
{
    using Exceptions;

    public abstract class Rule : IRule
    {
        /// <inheritdoc/>
        public abstract bool IsPasses { get; }

        /// <inheritdoc/>
        /// <exception cref="BrokenRuleException"/>
        public void EnsureCompliance()
        {
            if (!IsPasses)
            {
                throw new BrokenRuleException(ExceptionMessage);
            }
        }

        /// <summary>
        /// The message that is output in the <see cref="BrokenRuleException"/> when calling the <see cref="EnsureCompliance"/> if <see cref="IsPasses"/> returns <see langword="false"/>.
        /// </summary>
        protected virtual string ExceptionMessage => "Compliance with the rules is not ensured.";
    }
}
