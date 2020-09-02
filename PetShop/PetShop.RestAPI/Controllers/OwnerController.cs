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
    public class OwnerController : ControllerBase
    {
        private IOwnerService OwnerService;

        public OwnerController(IOwnerService ownerService)
        {
            this.OwnerService = ownerService;
        }

        [HttpPost]
        public ActionResult<Owner> CreateOwner([FromBody] Owner owner)
        {
            try
            {
                Owner ownerToAdd = OwnerService.CreateOwner(owner.FirstName,owner.LastName,owner.Address,owner.PhoneNumber,owner.Email);

                if (!OwnerService.AddOwner(ownerToAdd))
                {
                    return StatusCode(500, "Error saving pet to Database");
                }

                return Created("", ownerToAdd);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            IEnumerable<Owner> ownerEnumerable = OwnerService.GetOwnersFilterSearch(filter);

            if (ownerEnumerable.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(ownerEnumerable);
        }

        [HttpGet("{ID}")]
        public ActionResult<Owner> GetByID(int ID)
        {
            Owner owner = OwnerService.GetOwnerByID(ID);
            if (owner != null)
            {
                return Ok(owner);
            }
            return NoContent();
        }

        [HttpPut("{ID}")]
        public ActionResult<Owner> UpdateByID(int ID, [FromBody] Owner owner)
        {
            try
            {
                Owner ownerToUpdate = OwnerService.CreateOwner(owner.FirstName, owner.LastName, owner.Address, owner.PhoneNumber, owner.Email);
                Owner updatedOwner = OwnerService.UpdateOwner(ownerToUpdate, ID);

                if (updatedOwner == null)
                {
                    return StatusCode(500, "Error updating owner in Database");
                }
                return Accepted(updatedOwner);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public ActionResult<bool> DeleteByID(int ID)
        {
            if (OwnerService.GetOwnerByID(ID) == null)
            {
                return NotFound("No owner with such ID found");
            }
            try
            {
                return (OwnerService.DeleteOwner(ID)) ? Ok($"Owner with Id: {ID} successfully deleted") : StatusCode(500, $"Server error deleting owner with Id: {ID}");
            }
            catch(ArgumentException ex)
            {
                return StatusCode(500, $"Server error deleting owner with Id: {ID}");
            }
        }
    }
}
