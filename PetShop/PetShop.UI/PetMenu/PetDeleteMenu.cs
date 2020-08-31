using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;

namespace PetShop.UI
{
    public class PetDeleteMenu: Menu
    {
        private IPetService PetService;
        public PetDeleteMenu(IPetService petService) : base("Delete Menu", "Delete by ID", "Delete by Selection")
        {
            this.PetService = petService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    DeleteByID();
                    break;
                case 2:
                    DeleteBySelection();
                    break;
                default:
                    break;
            }
        }
        private void DeleteByID()
        {
            Console.WriteLine("\nPlease enter ID:");

            int ID;
            while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
            {
                Console.WriteLine("Please only enter a valid ID");
            }

            Pet foundPet = PetService.GetPetByID(ID);
            if(foundPet == null)
            {
                Console.WriteLine("\nError - no such ID found");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\nDo you want to delete\n\n{foundPet}\n");

                if (ConfirmChoise())
                {
                    Console.WriteLine((PetService.DeletePet(ID) ? "\nPet was successfully deleted!" : "\nError deleting pet. Please try again..."));
                }
            }
        }

        private void DeleteBySelection()
        {
            List<Pet> allPets = PetService.GetAllPets();

            Console.Clear();
            Console.WriteLine("\nPlease select which pet to delete:\n");
            int selection = GetOption<Pet>(allPets, true);
            
            if (selection > 0)
            {
                Console.Clear();
                Console.WriteLine($"\nDo you want to delete\n\n{allPets[selection-1]}\n");

                if (ConfirmChoise())
                {
                    Console.WriteLine((PetService.DeletePet(allPets[selection-1].ID) ? "\nPet was successfully deleted!" : "\nError deleting pet. Please try again..."));
                }
            }
        }
    }
}
