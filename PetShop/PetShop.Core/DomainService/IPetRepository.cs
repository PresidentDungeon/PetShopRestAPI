using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        bool AddPet(Pet pet);
        IEnumerable<Pet> ReadPets();
        bool UpdatePet(Pet pet);
        bool DeletePet(int ID);
    }
}
