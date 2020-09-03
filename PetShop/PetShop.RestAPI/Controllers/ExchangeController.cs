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
    [Route("api/[controller]")]
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

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            IEnumerable<Pet> petEnumerable;
            petEnumerable = PetExchangeService.ListAllPetsWithOwner();

            if (petEnumerable.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(petEnumerable);
        }

        [HttpGet("{ID}")]
        public ActionResult<IEnumerable<Pet>> GetPetsByOwner(int ID)
        {
            IEnumerable<Pet> petEnumerable;

            Owner owner = OwnerService.GetOwnerByID(ID);
            if (owner == null)
            {
                return BadRequest("No owner with such an ID exists");
            }

            petEnumerable = PetExchangeService.ListAllPetsRegisteredToOwner(ID);

            if (petEnumerable.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(petEnumerable);
        }

        [HttpPost("{petID},{ownerID}")]
        public ActionResult<Pet> RegisterPet(int petID, int ownerID)
        {
            Pet petToRegister = PetService.GetPetByID(petID);
            Owner ownerToRegister = OwnerService.GetOwnerByID(ownerID);

            if(petToRegister == null || ownerToRegister == null)
            {
                return BadRequest("No pet or owner with such an ID exists");
            }
            try
            {
                Pet updatedPet = PetExchangeService.RegisterPet(petToRegister, ownerToRegister);
                return (updatedPet != null) ? Accepted(updatedPet) : StatusCode(500, $"Server error when registering owner with Id: {ownerID} to pet with Id: {petID}");
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, $"Server error when registering owner with Id: {ownerID} to pet with Id: {petID}");
            }
        }

        [HttpPost("{petID}")]
        public ActionResult<Pet> UnregisterPet(int petID)
        {
            Pet petToUnregister = PetService.GetPetByID(petID);

            if (petToUnregister == null)
            {
                return BadRequest("No pet with such an ID exists");
            }
            try
            {
                Pet updatedPet = PetExchangeService.UnregisterPet(petToUnregister);
                return (updatedPet != null) ? Accepted(updatedPet) : StatusCode(500, $"Server error when unregistering pet with Id: {petID}");
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, $"Server error when unregistering pet with Id: {petID}");
            }
        }

    }
}
