using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        private int ID;
        private IEnumerable<Owner> Owners;

        public OwnerRepository()
        {
            this.ID = 0;
            this.Owners = new List<Owner>();
        }

        public Owner AddOwner(Owner owner)
        {
            ID++;
            owner.ID = ID;
            ((List<Owner>)Owners).Add(owner);
            return owner;
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return Owners;
        }

        public IEnumerable<Owner> ReadOwnersFilterSearch(Filter filter)
        {
            IEnumerable<Owner> owners = this.Owners;

            if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("asc"))
            {
                owners = from x in owners orderby x.FirstName + x.LastName select x;
            }
            else if (!string.IsNullOrEmpty(filter.Sorting) && filter.Sorting.ToLower().Equals("desc"))
            {
                owners = from x in owners orderby x.FirstName + x.LastName descending select x;
            }

            return owners;
        }

        public Owner GetOwnerByID(int ID)
        {
            return ReadOwners().Where((x) => { return x.ID == ID; }).FirstOrDefault();
        }

        public Owner UpdateOwner(Owner owner)
        {
            Owner ownerToUpdate = ((List<Owner>)Owners).Find((x) => { return x.ID == owner.ID; });
            if (ownerToUpdate != null)
            {
                ownerToUpdate.FirstName = owner.FirstName;
                ownerToUpdate.LastName = owner.LastName;
                ownerToUpdate.Address = owner.Address;
                ownerToUpdate.PhoneNumber = owner.PhoneNumber;
                ownerToUpdate.Email = owner.Email;

                return ownerToUpdate;
            }
            return null;
        }

        public Owner DeleteOwner(int ID)
        {
            Owner owner = Owners.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (owner != null)
            {
                ((List<Owner>)Owners).Remove(owner);
                return owner;
            }
            return null;
        }
    }
}
