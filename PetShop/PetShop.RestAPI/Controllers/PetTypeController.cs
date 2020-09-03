﻿using System;
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
    public class PetTypeController : ControllerBase
    {
        private IPetService PetService;
        private IOwnerService OwnerService;
        private IPetTypeService PetTypeService;

        public PetTypeController(IPetService petService, IOwnerService ownerService, IPetTypeService petTypeService)
        {
            this.PetService = petService;
            this.OwnerService = ownerService;
            this.PetTypeService = petTypeService;
        }

        [HttpPost]
        public ActionResult<Pet> CreatePetType([FromBody] PetType petType)
        {
            try
            {
                PetType petTypeToAdd = PetTypeService.CreatePetType(petType.Type);

                if (!PetTypeService.AddPetType(petTypeToAdd))
                {
                    return StatusCode(500, "Error saving pet to Database");
                }

                return Created("", petTypeToAdd);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery]Filter filter)
        {
            IEnumerable<PetType> petTypeEnumerable = PetTypeService.GetPetTypesFilterSearch(filter);

            if (petTypeEnumerable.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(petTypeEnumerable);
        }

        [HttpGet("{ID}")]
        public ActionResult<PetType> GetByID(int ID)
        {
            PetType type = PetTypeService.GetPetTypeByID(ID);
            if (type != null)
            {
                return type;
            }
            return NoContent();
        }

        [HttpPut("{ID}")]
        public ActionResult<PetType> UpdateByID(int ID, [FromBody] PetType type)
        {
            try
            {
                PetType petTypeToAUpdate = PetTypeService.CreatePetType(type.Type);
               PetType updatedPetType = PetTypeService.UpdatePetType(petTypeToAUpdate, ID);

                if(updatedPetType == null)
                {
                    return StatusCode(500, "Error updating pet type in Database");
                }
                return Accepted(updatedPetType);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public ActionResult<bool> DeleteByID(int ID)
        {
            if (PetTypeService.GetPetTypeByID(ID) == null)
            {
                return NotFound("No pet type with such ID found");
            }

            try
            {
                return (PetTypeService.DeletePetType(ID)) ? Ok($"Pet type with Id: {ID} successfully deleted") : StatusCode(500, $"Server error deleting pet type with Id: {ID}");
            }
            catch(ArgumentException ex)
            {
               return StatusCode(500, $"Server error deleting pet type with Id: {ID}");
            }
            
        }
    }
}