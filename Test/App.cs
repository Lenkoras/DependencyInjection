using DependencyInjection.Extensions;

var serviceDictionary = new ServiceDictionary();

serviceDictionary
    .AddSingleton<ICollection<Element>>(new List<Element>() { new("Hydrogenium"), new("Helium"), new("Carboneum"), new("Oxygenium"), new("Silicium") })
    .AddTransient<IWriter<Element>, ConsoleElementWriter>()
    .AddTransient<ICollectionWriter, CollectionWriter<Element>>();

IServiceProvider serviceProvider = serviceDictionary.BuildServiceProvider();

ICollectionWriter? room = serviceProvider.GetService<ICollectionWriter>();

room?.WriteAll();
