using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using PetShop.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private IPetRepository PetRepository;
        private ISearchEngine SearchEngine;

        public PetService(IPetRepository petRepository, ISearchEngine searchEngine)
        {
            this.PetRepository = petRepository;
            this.SearchEngine = searchEngine;
        }

        public Pet CreatePet(string petName, petType type, DateTime birthDate, string color, double price)
        {
            if (string.IsNullOrEmpty(petName))
            {
                throw new ArgumentException("Entered pet name too short");
            }
            if (string.IsNullOrEmpty(color))
            {
                throw new ArgumentException("Entered color description too short");
            }
            if(price < 0)
            {
                throw new ArgumentException("Pet price can't be negative");
            }

            return new Pet { Name = petName, Type = type, Birthdate = birthDate, Color = color, Price = price };
        }

        public bool AddPet(Pet pet)
        {
            if(pet != null)
            {
                return PetRepository.AddPet(pet);
            }
            return false;
        }

        public List<Pet> GetAllPets()
        {
            return PetRepository.ReadPets().ToList();
        }

        public List<Pet> GetAllPetsByPrice()
        {
            return PetRepository.ReadPets().OrderBy((x) => { return x.Price; }).ToList();
        }

        public List<Pet> GetAllAvailablePetsByPrice()
        {
            return (from x in GetAllPets() where x.Owner == null orderby x.Price select x).ToList();
        }

        public Pet GetPetByID(int ID)
        {
            return GetAllPets().Where((x) => { return x.ID == ID; }).FirstOrDefault();
        }

        public List<Pet> GetPetByType(petType type)
        {
            return (from x in GetAllPets() where x.Type.Equals(type) select x).ToList();
        }

        public List<Pet> GetPetByName(string searchTitle)
        {
            return SearchEngine.Search<Pet>(GetAllPets(), searchTitle);
;       }

        public List<Pet> GetPetByBirthdate(DateTime date)
        {
            return (from x in GetAllPets() where x.Birthdate.Equals(date) select x).ToList();
        }

        public bool UpdatePet(Pet pet, int ID)
        {
            if(pet != null)
            {
                pet.ID = ID;
                return PetRepository.UpdatePet(pet);
            }
            return false;
        }

        public bool DeletePet(int ID)
        {
            return PetRepository.DeletePet(ID);
        }
    }
}
