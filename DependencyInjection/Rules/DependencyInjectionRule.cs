namespace DependencyInjection.Rules
{
    /// <summary>
    /// Ensures avoid recursive dependency.
    /// </summary>
    public class DependencyInjectionRule : Rule, IDependencyInjectionRule
    {
        /// <inheritdoc/>
        public int MaxDependencyInjectionDepth { get; set; }        

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionRule"/> with specified <paramref name="dependencyDictionary"/> and <paramref name="maxDependencyInjectionDepth"/>.
        /// </summary>
        /// <param name="dependencyDictionary"></param>
        /// <param name="maxDependencyInjectionDepth"></param>
        public DependencyInjectionRule(IDictionary<int, IEnumerable<int>> dependencyDictionary, int maxDependencyInjectionDepth = DefaultMaxDependencyInjectionDepth)
        {
            ArgumentNullException.ThrowIfNull(dependencyDictionary);

            this.dependencyDictionary = dependencyDictionary;
            MaxDependencyInjectionDepth = maxDependencyInjectionDepth;
        }

        /// <inheritdoc/>
        public override bool IsPasses
        {
            get
            {
                CancellationTokenSource cts = new();
                return dependencyDictionary.Values.All(dependencies => !IsMoreThanMax(dependencies, dependencyChain: new(), 1, cts));
            }
        }

        /// <summary>
        /// Message that using in exception occurrences.
        /// </summary>
        protected override string ExceptionMessage => $"Recursive dependency avoidance is not ensured.";

        private IDictionary<int, IEnumerable<int>> dependencyDictionary;
        public const int DefaultMaxDependencyInjectionDepth = 32;

        private bool IsMoreThanMax(IEnumerable<int> dependencies, HashSet<int> dependencyChain, int currentDIDepth, CancellationTokenSource cts)
        {
            if (currentDIDepth > MaxDependencyInjectionDepth)
            {
                cts.Cancel();
                return true;
            }
            if (cts.IsCancellationRequested)
            {
                return false;
            }
            int previousCount = dependencyChain.Count;
            foreach (int currentDependencyType in dependencyDictionary.Keys)
            {
                if (dependencies.Contains(currentDependencyType))
                {
                    HashSet<int> currentSet = dependencyChain.ToHashSet();
                    currentSet.Add(currentDependencyType);

                    if (previousCount == currentSet.Count) // type not added
                    {
                        cts.Cancel();
                        return true;
                    }

                    if (dependencyDictionary.TryGetValue(currentDependencyType, out var currentDependencies) &&
                        IsMoreThanMax(currentDependencies, currentSet, currentDIDepth + 1, cts))
                    {
                        return true;
                    }
                    if (cts.IsCancellationRequested)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
