using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class PetTypeRepository: IPetTypeRepository
    {
        private int ID;
        private IEnumerable<PetType> PetTypes;

        public PetTypeRepository()
        {
            this.ID = 0;
            this.PetTypes = new List<PetType>();
        }

        public bool AddPetType(PetType type)
        {
            ID++;
            type.ID = ID;
            ((List<PetType>)PetTypes).Add(type);
            return true;
        }

        public IEnumerable<PetType> ReadTypes()
        {
            return PetTypes;
        }

        public PetType UpdatePetType(PetType type)
        {
            int index = ((List<PetType>)PetTypes).FindIndex((x) => { return x.ID == type.ID; });
            if (index != -1)
            {
                List<PetType> newPetTypes = PetTypes.ToList();
                newPetTypes[index] = type;
                PetTypes = newPetTypes;
                return type;
            }
            return null;
        }

        public bool DeletePetType(int ID)
        {
            PetType petType = PetTypes.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (petType != null)
            {
                ((List<PetType>)PetTypes).Remove(petType);
                return true;
            }
            return false;
        }
    }
}
