using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetExchangeService
    {
        bool RegisterPet(Pet pet, Owner owner);
        bool UnregisterPet(Pet pet);
        List<Pet> ListAllPetsRegisteredToOwner(int ID);
        List<Pet> ListAllPetsWithOwner();
    }
}
