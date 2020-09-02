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
        private IOwnerService OwnerService;

        public PetController(IPetService petService, IOwnerService ownerService)
        {
            this.PetService = petService;
            this.OwnerService = ownerService;
        }

        [HttpPost]
        public ActionResult<Pet> CreatePet([FromBody] Pet pet)
        {
            try
            {
                Pet petToAdd = PetService.CreatePet(pet.Name, pet.Type, pet.Birthdate, pet.Color, pet.Price);
                petToAdd.SoldDate = pet.SoldDate;

                if(pet.Owner != null)
                {
                    if(pet.Owner.ID <= 0)
                    {
                        return BadRequest("Owner ID can't be zero or negative");
                    }

                    Owner owner = OwnerService.GetOwnerByID(pet.Owner.ID);

                    if(owner == null)
                    {
                        return BadRequest("No owner with that ID found");
                    }
                    petToAdd.Owner = owner;
                }

                if (!PetService.AddPet(petToAdd))
                {
                    return StatusCode(500, "Error saving pet to Database");
                }

                return Created("", petToAdd);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get(string name, bool sorted)
        {
            IEnumerable<Pet> petEnumerable;

            if (string.IsNullOrEmpty(name))
            {
                if (sorted == true)
                {
                    petEnumerable = PetService.GetAllPetsByPrice();
                }

                petEnumerable = PetService.GetAllPets();
            }
            else
            {
                petEnumerable = PetService.GetPetByName(name);
            }

            if(petEnumerable.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(petEnumerable);
        }

        [HttpGet("{ID}")]
        public ActionResult<Pet> GetByID(int ID)
        {
            Pet pet = PetService.GetPetByID(ID);
            if (pet != null)
            {
                return pet;
            }
            return NoContent();
        }


        [HttpGet("Type/{PetType}")]
        public IEnumerable<Pet> GetByType(petType PetType)
        {
            return PetService.GetPetByType(PetType);
        }

        [HttpPut("{ID}")]
        public ActionResult<Pet> UpdateByID(int ID, [FromBody] Pet pet)
        {
            try
            {
                Pet petToAUpdate = PetService.CreatePet(pet.Name, pet.Type, pet.Birthdate, pet.Color, pet.Price);
                petToAUpdate.SoldDate = pet.SoldDate;

                if (pet.Owner != null)
                {
                    if (pet.Owner.ID <= 0)
                    {
                        return BadRequest("Owner ID can't be zero or negative");
                    }

                    Owner owner = OwnerService.GetOwnerByID(pet.Owner.ID);

                    if (owner == null)
                    {
                        return BadRequest("No owner with that ID found");
                    }
                    petToAUpdate.Owner = owner;
                }
               Pet updatedPet = PetService.UpdatePet(petToAUpdate, ID);

                if(updatedPet == null)
                {
                    return StatusCode(500, "Error updating pet in Database");
                }
                return Accepted(updatedPet);
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

            return (PetService.DeletePet(ID)) ? Ok($"Pet with Id: {ID} successfully deleted") : StatusCode(500,$"Server error deleting pet with Id: {ID}");
        }
    }
}
