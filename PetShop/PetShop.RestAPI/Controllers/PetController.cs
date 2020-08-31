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
    public class PetController : ControllerBase
    {
        private IPetService PetService;

        public PetController(IPetService petService)
        {
            this.PetService = petService;
        }

        [HttpPost]
        public ActionResult<bool> CreatePet([FromBody] Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("Entered pet name too short");
            }
            if (string.IsNullOrEmpty(pet.Color))
            {
                return BadRequest("Entered color description too short");
            }
            if (pet.Price < 0)
            {
                return BadRequest("Pet price can't be negative");
            }

            try
            {
                Pet petToAdd = PetService.CreatePet(pet.Name, pet.Type, pet.Birthdate, pet.Color, pet.Price);

                if(!PetService.AddPet(petToAdd))
                {
                    return StatusCode(500, "Error saving pet to Database");
                }

                return true;
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Pet> GetPets(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return PetService.GetAllPets();
            }
            return PetService.GetPetByName(name);
        }

        [HttpGet("{ID}")]
        public ActionResult<Pet> GetByID(int ID)
        {
            Pet pet = PetService.GetPetByID(ID);
            if(pet != null)
            {
                return pet;
            }
            return NotFound("No such pet found");
        }

        [HttpPut("{ID}")]
        public ActionResult<bool> UpdateByID(int ID, [FromBody] Pet pet)
        {
            if (PetService.GetPetByID(ID) == null)
            {
                return NotFound("No pet with such ID found");
            }

            try
            {
                Pet petToUpdate = PetService.CreatePet(pet.Name, pet.Type, pet.Birthdate, pet.Color, pet.Price);
                return PetService.UpdatePet(petToUpdate, ID);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public ActionResult<bool> DeleteByID(int ID)
        {
            if (PetService.GetPetByID(ID) == null)
            {
                return NotFound("No pet with such ID found");
            }

            return PetService.DeletePet(ID);
        }
    }
}
