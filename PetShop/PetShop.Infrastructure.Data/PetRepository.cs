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

        public bool AddPet(Pet pet)
        {
            ID++;
            pet.ID = ID;
            ((List<Pet>)Pets).Add(pet);
            return true;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return Pets;
        }

        public bool UpdatePet(Pet pet)
        {
            int index = ((List<Pet>)Pets).FindIndex((x) => { return x.ID == pet.ID; });
            if (index != -1)
            {
                List<Pet> newPets = Pets.ToList();
                newPets[index] = pet;
                Pets = newPets;
                return true;
            }
            return false;
        }

        public bool DeletePet(int ID)
        {
            Pet pet = Pets.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (pet != null)
            {
                ((List<Pet>)Pets).Remove(pet);
                return true;
            }
            return false;
        }
    }
}
