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

        public bool AddOwner(Owner owner)
        {
            ID++;
            owner.ID = ID;
            ((List<Owner>)Owners).Add(owner);
            return true;
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return Owners;
        }

        public bool UpdateOwner(Owner owner)
        {
            int index = ((List<Owner>)Owners).FindIndex((x) => { return x.ID == owner.ID; });
            if (index != -1)
            {
                List<Owner> newOwners = Owners.ToList();

                newOwners[index].FirstName = owner.FirstName;
                newOwners[index].LastName = owner.LastName;
                newOwners[index].Address = owner.Address;
                newOwners[index].PhoneNumber = owner.PhoneNumber;
                newOwners[index].Email = owner.Email;

                Owners = newOwners;

                return true;
            }
            return false;
        }

        public bool DeleteOwner(int ID)
        {
            Owner owner = Owners.Where((x) => { return x.ID == ID; }).FirstOrDefault();
            if (owner != null)
            {
                ((List<Owner>)Owners).Remove(owner);
                return true;
            }
            return false;
        }
    }
}
