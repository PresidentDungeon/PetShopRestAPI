using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;

namespace PetShop.UI
{
    public class PetSearchMenu : Menu
    {
        private IPetService PetService;
        public PetSearchMenu(IPetService petService) : base("Search Menu", "Search by ID", "Search by Name", "Search by Animal Type", "Search by Birthdate")
        {
            this.PetService = petService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    SearchByID();
                    break;
                case 2:
                    SearchByName();
                    break;
                case 3:
                    SearchByType();
                    break;
                case 4:
                    SearchByBirthdate();
                    break;
                default:
                    break;
            }
        }

        private void SearchByID()
        {
            Console.WriteLine("\nPlease enter ID:");

            int ID;
            while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
            {
                Console.WriteLine("\nPlease only enter a valid ID");
            }

            Pet pet = PetService.GetPetByID(ID);
            Console.Clear();
            Console.WriteLine((pet != null) ? $"\nFound pet:\n\n{pet}\n" : $"\nNo pet were found with that ID");
        }

        private void SearchByName()
        {
            Console.WriteLine("\nPlease enter a title (use % to break keywords):");
            List<Pet> foundPets = PetService.GetPetByName(Console.ReadLine());

            if (foundPets.Count == 0)
            {
                Console.WriteLine("\nNo pets were found with that name...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nMatches are-----------------------\n");
                foreach (Pet pet in foundPets)
                {
                    Console.WriteLine($"{pet}\n");
                }
            }
        }

        private void SearchByType()
        {
            Array petTypes = Enum.GetValues(typeof(petType));

            Console.WriteLine("\nSelect a pet type\n");
            int selection;

            for (int i = 0; i < petTypes.Length; i++)
            {
                Console.WriteLine(i + 1 + ": " + petTypes.GetValue(i));
            }
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > petTypes.Length)
            {
                Console.WriteLine($"Invalid input. Please choose an option in range (1-{petTypes.Length})");
            }

            petType petType = (petType)petTypes.GetValue(selection - 1);
            List<Pet> foundPets = PetService.GetPetByType(petType);
            if (foundPets.Count == 0)
            {
                Console.WriteLine("\nNo pets were found of that type...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nMatches are-----------------------\n");
                foreach (Pet pet in foundPets)
                {
                    Console.WriteLine($"{pet}\n");
                }
            }
        }

        private void SearchByBirthdate()
        {
            Console.WriteLine("\nEnter Birthdate:");
            DateTime birthDate;

            while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine("Please enter a valid birthdate (dd/mm/yyyy)");
            }

            List<Pet> foundPets = PetService.GetPetByBirthdate(birthDate);
            Console.Clear();
            if (foundPets.Count == 0)
            {
                Console.WriteLine("\nNo pets were found with that birthdate...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nMatches are-----------------------\n");
                foreach (Pet pet in foundPets)
                {
                    Console.WriteLine($"{pet}\n");
                }
            }
        }
    }
}
