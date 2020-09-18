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

        public PetType AddPetType(PetType type)
        {
            ID++;
            type.ID = ID;
            ((List<PetType>)PetTypes).Add(type);
            return type;
        }

        public IEnumerable<PetType> ReadTypes()
        {
            return PetTypes;
        }

        public IEnumerable<PetType> ReadTypesFilterSearch(Filter filter)
        {
            IEnumerable<PetType> types = this.PetTypes;

            if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("asc"))
            {
                types = from x in types orderby x.Name select x;
            }
            else if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("desc"))
            {
                types = from x in types orderby x.Name descending select x;
            }

            return types;
        }

        public PetType GetPetTypeByID(int ID)
        {
            return ReadTypes().Where((x) => { return x.ID == ID; }).FirstOrDefault();
        }


        public PetType UpdatePetType(PetType type)
        {
            PetType petType = ((List<PetType>)PetTypes).Find((x) => { return x.ID == type.ID; });
            if (petType != null)
            {
                petType.Name = type.Name;
                return petType;
            }
            return null;
        }

        public PetType DeletePetType(int ID)
        {
            PetType petType = PetTypes.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (petType != null)
            {
                ((List<PetType>)PetTypes).Remove(petType);
                return petType;
            }
            return null;
        }
    }
}
