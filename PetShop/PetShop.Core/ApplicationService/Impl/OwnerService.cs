﻿using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using PetShop.Core.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService: IOwnerService
    {
        private IOwnerRepository OwnerRepository;
        private IPetRepository PetRepository;
        private ISearchEngine SearchEngine;

        public OwnerService(IOwnerRepository ownerRepository, IPetRepository petRepository, ISearchEngine searchEngine)
        {
            this.OwnerRepository = ownerRepository;
            this.PetRepository = petRepository;
            this.SearchEngine = searchEngine;
        }

        public Owner CreateOwner(string firstName, string lastName, string address, string phoneNumber, string email)
        {
            int minAddressLenght = 5;
            int minPhoneNumberLenght = 8;

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Entered owner first name too short");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Entered owner last name too short");
            }
            if (string.IsNullOrEmpty(address) || address.Length < minAddressLenght)
            {
                throw new ArgumentException("Entered address too short");
            }
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < minPhoneNumberLenght)
            {
                throw new ArgumentException("Entered phone number too short");
            }

            return new Owner { FirstName = firstName, LastName = lastName, Address = address, PhoneNumber = phoneNumber, Email = email };
        }

        public Owner AddOwner(Owner owner)
        {
            if (owner != null)
            {
                return OwnerRepository.AddOwner(owner);
            }
            return null;
        }

        public List<Owner> GetAllOwners()
        {
            return OwnerRepository.ReadOwners().ToList();
        }

        public List<Owner> GetOwnersFilterSearch(Filter filter)
        {
            IEnumerable<Owner> owners = OwnerRepository.ReadOwnersFilterSearch(filter);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                return SearchEngine.Search<Owner>(owners.ToList(), filter.Name);
            }

            return owners.ToList();
        }

        public Owner GetOwnerByID(int ID)
        {
            return OwnerRepository.GetOwnerByID(ID);
        }

        public Owner GetOwnerByIDWithPets(int ID)
        {
            Owner owner = GetAllOwners().Select(x => new Owner()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Email = x.Email
            }).Where((x) => { return x.ID == ID; }).FirstOrDefault();

            if(owner != null)
            {
                List<Pet> pets = (from x in PetRepository.ReadPets() where x.Owner != null && x.Owner.ID == ID select x).ToList();
                owner.Pets = new List<Pet>();

                foreach (Pet pet in pets)
                {
                    owner.Pets.Add(new Pet
                    {
                        ID = pet.ID,
                        Name = pet.Name,
                        Type = pet.Type,
                        Birthdate = pet.Birthdate,
                        SoldDate = pet.SoldDate,
                        Color = pet.Color,
                        Price = pet.Price
                    });
                }
                return owner;
            }
            return null;
        }

        public List<Owner> GetOwnerByName(string searchTitle)
        {
            return SearchEngine.Search<Owner>(GetAllOwners(), searchTitle);
        }

        public Owner UpdateOwner(Owner owner, int ID)
        {
            if (GetOwnerByID(ID) == null)
            {
                throw new ArgumentException("No owner with such ID found");
            }
            if (owner == null)
            {
                throw new ArgumentException("Updating owner does not excist");
            }
            owner.ID = ID;
            return OwnerRepository.UpdateOwner(owner);
        }

        public Owner DeleteOwner(int ID)
        {
            if (ID <= 0)
            {
                throw new ArgumentException("Incorrect ID entered");
            }
            if (GetOwnerByID(ID) == null)
            {
                throw new ArgumentException("No owner with such ID found");
            }
            return OwnerRepository.DeleteOwner(ID);
        }
    }
}
