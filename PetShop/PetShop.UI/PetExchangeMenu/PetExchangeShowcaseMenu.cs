using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.UI
{
    public class PetExchangeShowcaseMenu: Menu
    {
        private IPetService PetService;
        private IOwnerService OwnerService;
        private IPetExchangeService PetExchangeService;
        public PetExchangeShowcaseMenu(IPetService petService, IOwnerService ownerService, IPetExchangeService petExchangeService) : base("Showcase Menu", "Display by ID", "Display by Selection")
        {
            this.PetService = petService;
            this.OwnerService = ownerService;
            this.PetExchangeService = petExchangeService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    DisplayPetsByID();
                    break;
                case 2:
                    DisplayPetsBySelection();
                    break;
                default:
                    break;
            }
        }

        private void DisplayPetsByID()
        {
            Console.WriteLine("\nPlease enter owner ID:");

            int ID;
            while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
            {
                Console.WriteLine("\nPlease only enter a valid ID");
            }

            Owner foundOwner = OwnerService.GetOwnerByID(ID);

            if (foundOwner == null)
            {
                Console.WriteLine("\nNo owner found with that ID");
                return;
            }

            List<Pet> foundPets = PetExchangeService.ListAllPetsRegisteredToOwner(ID);

            Console.Clear();
            Console.WriteLine($"\nAll pets registered to {foundOwner.FirstName} {foundOwner.LastName}:\n");
            foreach (Pet pet in foundPets)
            {
                Console.WriteLine(pet + "\n");
            }
        }

        private void DisplayPetsBySelection()
        {
            List<Owner> allOwners = OwnerService.GetAllOwners();
            Console.Clear();
            Console.WriteLine("\nPlease select which owner to view:\n");
            int selection = GetOption<Owner>(allOwners, true);

            if (selection > 0)
            {
                Owner selectedOwner = allOwners[selection - 1];
                List<Pet> foundPets = PetExchangeService.ListAllPetsRegisteredToOwner(selectedOwner.ID);

                Console.Clear();
                Console.WriteLine($"\nAll pets registered to {selectedOwner.FirstName} {selectedOwner.LastName}:\n");
                foreach (Pet pet in foundPets)
                {
                    Console.WriteLine(pet + "\n");
                }
            }
        }
    }
}
