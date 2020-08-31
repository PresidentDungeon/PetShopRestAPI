using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.UI
{
    public class PetExchangeUnregisterMenu: Menu
    {
        private IPetService PetService;
        private IPetExchangeService PetExchangeService;

        public PetExchangeUnregisterMenu(IPetService petService, IPetExchangeService petExchangeService) : base("Unregister Menu", "Unregister by ID", "Unregister by Selection")
        {
            this.PetService = petService;
            this.PetExchangeService = petExchangeService;
            ShouldCloseOnFinish = true;
        }

        protected override void DoAction(int option)
        {
            switch (option)
            {
                case 1:
                    UnregisterByID();
                    break;
                case 2:
                    UnregisterBySelection();
                    break;
                default:
                    break;
            }
        }

        private void UnregisterByID()
        {
            Console.WriteLine("\nPlease enter Pet ID:");

            int petID;
            while (!int.TryParse(Console.ReadLine(), out petID) || petID <= 0)
            {
                Console.WriteLine("\nPlease only enter a valid ID");
            }

            Console.Clear();

            Pet pet = PetService.GetPetByID(petID);

            if (pet == null || pet.Owner == null)
            {
                Console.WriteLine("\nNo pet or registered owner found with that ID");
                return;
            }

            Console.WriteLine($"\nDo you want to remove\n\n{pet.Owner}\n\nas a owner to\n\n{pet}\n");

            if (ConfirmChoise())
            {
                Console.WriteLine((PetExchangeService.UnregisterPet(pet)) ? "\nOwner successfully removed as owner to pet" : "\nError unregistering owner to pet. Please try again");
            }
        }

        private void UnregisterBySelection()
        {
            List<Pet> allOwnedPets = PetExchangeService.ListAllPetsWithOwner();
            Pet selectedPet;

            Console.Clear();
            Console.WriteLine("\nPlease select which pet to unregister owner:\n");
            int petSelection = GetOption<Pet>(allOwnedPets, true);

            if (petSelection == 0)
            {
                return;
            }

            selectedPet = allOwnedPets[petSelection - 1];

            Console.Clear();
            Console.WriteLine($"\nDo you want to unregister\n\n{selectedPet.Owner}\n\nas a owner to\n\n{selectedPet}\n");

            if (ConfirmChoise())
            {
                Console.WriteLine((PetExchangeService.UnregisterPet(selectedPet)) ? "\nOwner successfully removed as owner to pet" : "\nError unregistering owner to pet. Please try again");
            }
        }
    }
}
