using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;

namespace PetShop.UI
{
    public class OwnerDeleteMenu: Menu
    {
        private IOwnerService OwnerService;
        public OwnerDeleteMenu(IOwnerService ownerService) : base("Delete Menu", "Delete by ID", "Delete by Selection")
        {
            this.OwnerService = ownerService;
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

            Owner foundOwner = OwnerService.GetOwnerByID(ID);
            if (foundOwner == null)
            {
                Console.WriteLine("\nError - no such ID found");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\nDo you want to delete\n\n{foundOwner}\n");

                if (ConfirmChoise())
                {
                    Console.WriteLine((OwnerService.DeleteOwner(ID) ? "\nOwner was successfully deleted!" : "\nError deleting owner. Please try again..."));
                }
            }
        }

        private void DeleteBySelection()
        {
            List<Owner> allOwners = OwnerService.GetAllOwners();

            Console.Clear();
            Console.WriteLine("\nPlease select which owner to delete:\n");
            int selection = GetOption<Owner>(allOwners, true);

            if (selection > 0)
            {
                Console.Clear();
                Console.WriteLine($"\nDo you want to delete\n\n{allOwners[selection - 1]}\n");

                if (ConfirmChoise())
                {
                    Console.WriteLine((OwnerService.DeleteOwner(allOwners[selection - 1].ID) ? "\nOwner was successfully deleted!" : "\nError deleting owner. Please try again..."));
                }
            }
        }
    }
}
