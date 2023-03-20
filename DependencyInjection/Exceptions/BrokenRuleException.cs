namespace DependencyInjection.Exceptions
{
    public class BrokenRuleException : Exception
    {
        public BrokenRuleException() : this("Rule was broken.") { }

        public BrokenRuleException(string? message) : base(message) { }
    }
}
