using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetTypeRepository
    {
        bool AddPetType(PetType type);
        IEnumerable<PetType> ReadTypes();
        PetType UpdatePetType(PetType type);
        bool DeletePetType(int ID);
    }
}
