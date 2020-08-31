using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.UI
{
    public class PetExchangeRegisterMenu: Menu
    {
        private IPetService PetService;
        private IOwnerService OwnerService;
        private IPetExchangeService PetExchangeService;

        public PetExchangeRegisterMenu(IPetService petService, IOwnerService ownerService, IPetExchangeService petExchangeService) : base("Register Menu", "Register by ID", "Register by Selection")
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
                    RegisterByID();
                    break;
                case 2:
                    RegisterBySelection();
                    break;
                default:
                    break;
            }
        }

        private void RegisterByID()
        {
            Console.WriteLine("\nPlease enter Pet ID:");

            int petID;
            while (!int.TryParse(Console.ReadLine(), out petID) || petID <= 0)
            {
                Console.WriteLine("\nPlease only enter a valid ID");
            }

            Console.WriteLine("\nPlease enter Owner ID:");

            int ownerID;
            while (!int.TryParse(Console.ReadLine(), out ownerID) || ownerID <= 0)
            {
                Console.WriteLine("\nPlease only enter a valid ID");
            }

            Console.Clear();

            Pet pet = PetService.GetPetByID(petID);
            Owner owner = OwnerService.GetOwnerByID(ownerID);
            
            if(pet == null || owner == null)
            {
                Console.WriteLine("\nNo pet or owner found with that ID");
                return;
            }

            Console.WriteLine($"\nDo you want to register\n\n{pet}\n\nto\n\n{owner}\n");
           
            if(ConfirmChoise())
            {
                Console.WriteLine((PetExchangeService.RegisterPet(pet, owner)) ? "\nPet successfully registered to owner" : "\nError registering pet to owner. Please try again");
            }
        }

        private void RegisterBySelection()
        {
            List<Owner> allOwners = OwnerService.GetAllOwners();
            List<Pet> allPets = PetService.GetAllPets();
            Pet selectedPet;
            Owner selectedOwner;

            Console.Clear();
            Console.WriteLine("\nPlease select which pet to register:\n");
            int petSelection = GetOption<Pet>(allPets, true);

            if (petSelection == 0)
            {
                return;
            }

            selectedPet = allPets[petSelection - 1];

            Console.Clear();
            Console.WriteLine("\nPlease select which owner to register to:\n");
            int ownerSelection = GetOption<Owner>(allOwners, true);

            if (ownerSelection == 0)
            {
                return;
            }

            selectedOwner = allOwners[ownerSelection - 1];

            Console.Clear();
            Console.WriteLine($"\nDo you want to register\n\n{selectedPet}\n\nto\n\n{selectedOwner}\n");

            if (ConfirmChoise())
            {
                Console.WriteLine((PetExchangeService.RegisterPet(selectedPet, selectedOwner)) ? "\nPet successfully registered to owner" : "\nError registering pet to owner. Please try again");
            }
        }
    }
}
