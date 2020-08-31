using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        bool AddOwner(Owner owner);
        IEnumerable<Owner> ReadOwners();
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(int id);
    }
}
