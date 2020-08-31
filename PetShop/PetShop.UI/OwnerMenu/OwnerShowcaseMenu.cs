using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.UI
{
    public class OwnerShowcaseMenu : Menu
    {
        private IOwnerService OwnerService;
        public OwnerShowcaseMenu(IOwnerService ownerService) : base("Showcase Menu", "View Owners", "Search by ID", "Search by Name")
        {
            this.OwnerService = ownerService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    ShowAllOwners();
                    break;
                case 2:
                    SearchByID();
                    break;
                case 3:
                    SearchByName();
                    break;
                default:
                    break;
            }
        }

        private void ShowAllOwners()
        {
            Console.Clear();
            Console.WriteLine("\nAll registered owners are:\n");
            foreach (Owner owner in OwnerService.GetAllOwners())
            {
                Console.WriteLine($"{owner}\n");
            }
        }

        private void SearchByID()
        {
            Console.WriteLine("\nPlease enter ID:");

            int ID;
            while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
            {
                Console.WriteLine("Please only enter a valid ID");
            }

            Owner owner = OwnerService.GetOwnerByID(ID);
            Console.Clear();
            Console.WriteLine((owner != null) ? $"\nFound owner:\n\n{owner}\n" : $"\nNo owner with that ID were found");
        }


        private void SearchByName()
        {
            Console.WriteLine("\nPlease enter a title (use % to break keywords):");
            List<Owner> foundOwners = OwnerService.GetOwnerByName(Console.ReadLine());

            if (foundOwners.Count == 0)
            {
                Console.WriteLine("\nNo owners were found with that name...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nMatches are-----------------------\n");
                foreach (Owner owner in foundOwners)
                {
                    Console.WriteLine($"{owner}\n");
                }
            }
        }
    }
}
