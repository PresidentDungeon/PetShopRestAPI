using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Core.Search;
using PetShop.Infrastructure.Data;

namespace PetShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IOwnerService, OwnerService>();
            serviceCollection.AddScoped<IPetExchangeService, PetExchangeService>();
            serviceCollection.AddScoped<ISearchEngine, SearchEngine>();

            serviceCollection.AddScoped<MainMenu>();
            serviceCollection.AddScoped<PetMainMenu>();
            serviceCollection.AddScoped<PetShowcaseMenu>();
            serviceCollection.AddScoped<PetSearchMenu>();
            serviceCollection.AddScoped<PetDeleteMenu>();
            serviceCollection.AddScoped<OwnerMainMenu>();
            serviceCollection.AddScoped<OwnerShowcaseMenu>();
            serviceCollection.AddScoped<OwnerDeleteMenu>();
            serviceCollection.AddScoped<PetExchangeMainMenu>();
            serviceCollection.AddScoped<PetExchangeRegisterMenu>();
            serviceCollection.AddScoped<PetExchangeUnregisterMenu>();
            serviceCollection.AddScoped<PetExchangeShowcaseMenu>();
            serviceCollection.AddScoped<InitStaticData>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var initStaticData = serviceProvider.GetRequiredService<InitStaticData>(); 
            var mainMenu = serviceProvider.GetRequiredService<MainMenu>();

            initStaticData.InitData();
            mainMenu.Run();
        }
    }
}
