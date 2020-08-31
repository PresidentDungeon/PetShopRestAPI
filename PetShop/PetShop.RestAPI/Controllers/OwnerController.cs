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
        public void CreateOwner([FromBody] Owner owner)
        {
            OwnerService.AddOwner(owner);
        }

        [HttpGet]
        public IEnumerable<Owner> GetAllOwner()
        {
            return OwnerService.GetAllOwners();
        }

        [HttpGet("{ID}")]
        public Owner GetByID(int ID)
        {
            return OwnerService.GetOwnerByID(ID);
        }

        [HttpPut("{ID}")]
        public void UpdateByID(int ID, [FromBody] Owner owner)
        {
            OwnerService.UpdateOwner(owner, ID);
        }

        [HttpDelete("{ID}")]
        public void DeleteByID(int ID)
        {
            OwnerService.DeleteOwner(ID);
        }
    }
}
