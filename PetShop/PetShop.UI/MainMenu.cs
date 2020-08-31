using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.Entities;
using System;

namespace PetShop.UI
{
    public class MainMenu: Menu
    {
        private IServiceProvider ServiceProvider;

        public MainMenu(IServiceProvider serviceProvider) : base("Main Menu", "Pet Menu", "Owner Menu", "Pet Exhange Menu")
        {
            this.ServiceProvider = serviceProvider;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    ServiceProvider.GetRequiredService<PetMainMenu>().Run();
                    break;
                case 2:
                    ServiceProvider.GetRequiredService<OwnerMainMenu>().Run();
                    break;
                case 3:
                    ServiceProvider.GetRequiredService<PetExchangeMainMenu>().Run();
                    break;
                default:
                    break;
            }
        }
    }
}
