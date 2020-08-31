using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;

namespace PetShop.UI
{
    public class PetMainMenu: Menu
    {
        private IPetService PetService;
        private IServiceProvider ServiceProvider;

        public PetMainMenu(IPetService petService, IServiceProvider serviceProvider) : base("Pet Menu", "Add Pet", "View Pet", "Search Pet", "Update Pet", "Delete Pet")
        {
            this.PetService = petService;
            this.ServiceProvider = serviceProvider;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    AddPet(CreatePet());
                    Pause();
                    break;
                case 2:
                    ServiceProvider.GetRequiredService<PetShowcaseMenu>().Run();
                    Pause();
                    break;
                case 3:
                    ServiceProvider.GetRequiredService<PetSearchMenu>().Run();
                    Pause();
                    break;
                case 4:
                    UpdatePet();
                    Pause();
                    break;
                case 5:
                    ServiceProvider.GetRequiredService<PetDeleteMenu>().Run();
                    Pause();
                    break;
                default:
                    break;
            }
        }

        private Pet CreatePet()
        {
            Array petTypes = Enum.GetValues(typeof(petType));

            Console.WriteLine("\nEnter pet name:");
            string petName = Console.ReadLine();

            while (petName.Length <= 0)
            {
                Console.WriteLine("\nPlease enter a valid name");
                petName = Console.ReadLine();
            }

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

            Console.WriteLine("\nEnter birthdate:");
            DateTime birthDate;

            while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine("Please enter a valid release date (dd/mm/yyyy)");
            }

            Console.WriteLine("\nEnter pet color:");
            string petColor = Console.ReadLine();

            while (petColor.Length <= 0)
            {
                Console.WriteLine("\nPlease enter a valid color or description");
                petColor = Console.ReadLine();
            }

            Console.WriteLine("\nEnter pet price:");
            double petPrice;

            while (!double.TryParse(Console.ReadLine(), out petPrice) || petPrice < 0)
            {
                Console.WriteLine("\nPlease enter a valid price");
            }

            try 
            { 
                return PetService.CreatePet(petName, petType, birthDate, petColor, petPrice); 
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($"\n{ex.Message}");
                return null;
            }
        }

        private void AddPet(Pet pet)
        {
            if (PetService.AddPet(pet))
            {
                Console.WriteLine("\nPet was successfully added!");
            }
        }

        private void UpdatePet()
        {
            List<Pet> allPets = PetService.GetAllPets();

            Console.Clear();
            Console.WriteLine("\nPlease select which pet to update:\n");
            int selection = GetOption<Pet>(allPets, true);

            if (selection > 0)
            {
                Pet selectedPet = allPets[selection - 1];
                Pet newPet = CreatePet();
                newPet.ID = selectedPet.ID;
                newPet.Owner = selectedPet.Owner;
                newPet.SoldDate = selectedPet.SoldDate;
                Console.Clear();

                Console.WriteLine($"\nDo you want to update\n\n{selectedPet}\n\nto\n\n{newPet}\n");

                if (ConfirmChoise())
                {
                    Console.WriteLine((PetService.UpdatePet(newPet, selectedPet.ID)) ? "\nPet was successfully updated!" : "\nError updating pet. Please try again...");
                }
            }
        }
    }
}
