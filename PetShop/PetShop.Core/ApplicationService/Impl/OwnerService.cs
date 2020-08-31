using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using PetShop.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService: IOwnerService
    {
        private IOwnerRepository OwnerRepository;
        private ISearchEngine SearchEngine;

        public OwnerService(IOwnerRepository ownerRepository, ISearchEngine searchEngine)
        {
            this.OwnerRepository = ownerRepository;
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

        public bool AddOwner(Owner owner)
        {
            if (owner != null)
            {
                return OwnerRepository.AddOwner(owner);
            }
            return false;
        }

        public List<Owner> GetAllOwners()
        {
            return OwnerRepository.ReadOwners().ToList();
        }

        public Owner GetOwnerByID(int ID)
        {
            return GetAllOwners().Where((x) => { return x.ID == ID; }).FirstOrDefault();
        }

        public List<Owner> GetOwnerByName(string searchTitle)
        {
            return SearchEngine.Search<Owner>(GetAllOwners(), searchTitle);
        }

        public bool UpdateOwner(Owner owner, int ID)
        {
            if (owner != null)
            {
                owner.ID = ID;
                return OwnerRepository.UpdateOwner(owner);
            }
            return false;
        }

        public bool DeleteOwner(int ID)
        {
            return OwnerRepository.DeleteOwner(ID);
        }
    }
}
