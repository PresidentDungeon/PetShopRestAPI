using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        Pet CreatePet(string petName, petType type, DateTime birthDate, string color, double price);

        bool AddPet(Pet pet);

        List<Pet> GetAllPets();

        List<Pet> GetAllPetsByPrice();

        List<Pet> GetAllAvailablePetsByPrice();

        Pet GetPetByID(int ID);

        List<Pet> GetPetByType (petType type);

        List<Pet> GetPetByName(string searchTitle);

        List<Pet> GetPetByBirthdate(DateTime date);

        bool UpdatePet(Pet pet, int ID);

        bool DeletePet(int ID);


    }
}
