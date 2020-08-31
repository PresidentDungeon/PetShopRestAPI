using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class InitStaticData
    {
        private IPetRepository PetRepository;
        private IOwnerRepository OwnerRepository;

        public InitStaticData(IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            this.PetRepository = petRepository;
            this.OwnerRepository = ownerRepository;
        }
        public void InitData()
        {
            PetRepository.AddPet(new Pet
            {
                Name = "Hr. Dingles",
                Type = petType.Cat,
                Birthdate = DateTime.Parse("29-03-2012"),
                Color = "White with black stripes",
                Price = 750.0
            });
            PetRepository.AddPet(new Pet
            {
                Name = "SlowPoke",
                Type = petType.Turtle,
                Birthdate = DateTime.Parse("15-01-1982"),
                Color = "Dark green",
                Price = 365.25
            });
            PetRepository.AddPet(new Pet
            {
                Name = "Leggy",
                Type = petType.Tarantula,
                Birthdate = DateTime.Parse("05-08-2019"),
                Color = "Brown with orange dots",
                Price = 650.0
            });

            OwnerRepository.AddOwner(new Owner
            {
                FirstName = "Mathias",
                LastName = "Thomsen",
                Address = "Tulipanvej 33",
                PhoneNumber = "42411722",
                Email = "MathiasThomsen@gmail.com"
            });
            OwnerRepository.AddOwner(new Owner
            {
                FirstName = "Josefine",
                LastName = "Thulstrup",
                Address = "Kastanievej 17",
                PhoneNumber = "23221119",
                Email = "SejeJozze@hotmail.com"
            });
        }
    }
}
