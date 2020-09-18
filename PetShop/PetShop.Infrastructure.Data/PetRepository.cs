using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Infrastructure.Data
{
    public class PetRepository: IPetRepository
    {
        private int ID;
        private IEnumerable<Pet> Pets;

        public PetRepository()
        {
            this.ID = 0;
            this.Pets = new List<Pet>();
        }

        public Pet AddPet(Pet pet)
        {
            ID++;
            pet.ID = ID;
            ((List<Pet>)Pets).Add(pet);
            return pet;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return Pets;
        }

        public IEnumerable<Pet> ReadPetsFilterSearch(Filter filter)
        {
            IEnumerable<Pet> pets = this.Pets;

            if (!string.IsNullOrEmpty(filter.PetType))
            {
                pets = from x in pets where x.Type.Name.ToLower().Equals(filter.PetType.ToLower()) select x;
            }
            if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("asc"))
            {
                pets = from x in pets where x.Owner == null orderby x.Price select x;
            }
            else if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("desc"))
            {
                pets = from x in pets where x.Owner == null orderby x.Price descending select x;
            }

            return pets.AsEnumerable();
        }

        public Pet GetPetByID(int ID)
        {
            return ReadPets().Where((x) => { return x.ID == ID; }).FirstOrDefault();
        }

        public Pet UpdatePet(Pet pet)
        {
            int index = ((List<Pet>)Pets).FindIndex((x) => { return x.ID == pet.ID; });
            if (index != -1)
            {
                List<Pet> newPets = Pets.ToList();
                newPets[index] = pet;
                Pets = newPets;
                return pet;
            }
            return null;
        }

        public Pet DeletePet(int ID)
        {
            Pet pet = Pets.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (pet != null)
            {
                ((List<Pet>)Pets).Remove(pet);
                return pet;
            }
            return null;
        }

    }
}
