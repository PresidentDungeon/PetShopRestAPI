using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.UI
{
    public class PetExchangeMainMenu : Menu
    {
        private IServiceProvider ServiceProvider;

        public PetExchangeMainMenu(IServiceProvider serviceProvider) : base("Exchange Menu", "Register Owner to Pet", "Unregister Owner to Pet", "View Owner Registered Pets")
        {
            this.ServiceProvider = serviceProvider;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    ServiceProvider.GetRequiredService<PetExchangeRegisterMenu>().Run();
                    Pause();
                    break;
                case 2:
                    ServiceProvider.GetRequiredService<PetExchangeUnregisterMenu>().Run();
                    Pause();
                    break;
                case 3:
                    ServiceProvider.GetRequiredService<PetExchangeShowcaseMenu>().Run();
                    Pause();
                    break;
                default:
                    break;
            }
        }
    }
}
