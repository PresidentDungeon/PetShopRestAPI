using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;

namespace PetShop.UI
{
    public class PetShowcaseMenu: Menu
    {
        private IPetService PetService;
        public PetShowcaseMenu(IPetService petService) : base("Showcase Menu", "Display all Pets", "Display by Price", "Display Available by Price", "Display top five Cheapest Available Pets")
        {
            this.PetService = petService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    ShowAllPets();
                    break;
                case 2:
                    ShowAllPetsByPrice();
                    break;
                case 3:
                    ShowAllAvailablePetsByPrice();
                    break;
                case 4:
                    DisplayTopFive();
                    break;
                default:
                    break;
            }
        }

        private void ShowAllPets()
        {
            Console.Clear();
            Console.WriteLine("\nAll registered pets are:\n");
            foreach (Pet pet in PetService.GetAllPets())
            {
                Console.WriteLine(pet + "\n");
            }
        }

        private void ShowAllPetsByPrice()
        {
            Console.Clear();
            Console.WriteLine("\nAll registered pets by price are:\n");
            foreach (Pet pet in PetService.GetAllPetsByPrice())
            {
                Console.WriteLine(pet + "\n");
            }
        }

        private void ShowAllAvailablePetsByPrice()
        {
            Console.Clear();
            Console.WriteLine("\nAll available pets by price are:\n");
            foreach (Pet pet in PetService.GetAllAvailablePetsByPrice())
            {
                Console.WriteLine(pet + "\n");
            }
        }

        private void DisplayTopFive()
        {
            Console.Clear();
            List<Pet> sortedList = PetService.GetAllAvailablePetsByPrice();
            int availableSize = sortedList.Count;

            if (availableSize == 0)
            {
                Console.WriteLine("\nSorry, there are no available pets at the moment...");
            }
            else
            {
                Console.WriteLine((availableSize<5) ? $"\nThere are only {availableSize} pets available:\n" : "\nTop five cheapest available pets are:\n");
            
                for (int i = 0; i < availableSize; i++)
                {
                 Console.WriteLine(sortedList[i] + "\n");
                }
            }
        }
    }
}
