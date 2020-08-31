using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        Owner CreateOwner(string firstName, string lastName, string address, string phoneNumber, string email);

        bool AddOwner(Owner owner);

        List<Owner> GetAllOwners();

        Owner GetOwnerByID(int ID);

        List<Owner> GetOwnerByName(string searchTitle);

        bool UpdateOwner(Owner owner, int ID);

        bool DeleteOwner(int ID);
    }
}
