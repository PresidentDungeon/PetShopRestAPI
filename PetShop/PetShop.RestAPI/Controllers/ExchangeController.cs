using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using PetShop.Infrastructure.Data;

namespace PetShop.RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {
        private IPetService PetService;
        private IOwnerService OwnerService;
        private IPetExchangeService PetExchangeService;

        public ExchangeController(IPetService petService, IOwnerService ownerService, IPetExchangeService petExchangeService)
        {
            this.PetService = petService;
            this.OwnerService = ownerService;
            this.PetExchangeService = petExchangeService;
        }

        [HttpPost]
        public void RegisterPet(int petID, int ownerID)
        {
            Pet petToRegister = PetService.GetPetByID(petID);
            Owner ownerToRegister = OwnerService.GetOwnerByID(ownerID);

            PetExchangeService.RegisterPet(petToRegister, ownerToRegister);
        }

    }
}
