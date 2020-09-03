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
        Pet UpdatePet(Pet pet);
        Pet DeletePet(int ID);
    }
}
