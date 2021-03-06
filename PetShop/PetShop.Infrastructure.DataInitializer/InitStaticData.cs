﻿using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class InitStaticData
    {
        private IPetRepository PetRepository;
        private IOwnerRepository OwnerRepository;
        private IPetTypeRepository PetTypeRepository;

        public InitStaticData(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            this.PetRepository = petRepository;
            this.OwnerRepository = ownerRepository;
            this.PetTypeRepository = petTypeRepository;
        }
        public void InitData()
        {
            PetType cat = new PetType { Name = "Cat" };
            PetType dog = new PetType { Name = "Dog" };
            PetType fish = new PetType { Name = "Fish" };
            PetType lizard = new PetType { Name = "Lizard" };
            PetType tarantula = new PetType { Name = "Tarantula" };
            PetType turtle = new PetType { Name = "Turtle" };
            PetType goat = new PetType { Name = "Goat" };

            PetTypeRepository.AddPetType(cat);
            PetTypeRepository.AddPetType(dog);
            PetTypeRepository.AddPetType(fish);
            PetTypeRepository.AddPetType(lizard);
            PetTypeRepository.AddPetType(tarantula);
            PetTypeRepository.AddPetType(turtle);
            PetTypeRepository.AddPetType(goat);

            PetRepository.AddPet(new Pet
            {
                Name = "Hr. Dingles",
                Type = cat,
                Birthdate = DateTime.Parse("29-03-2012", CultureInfo.GetCultureInfo("da-DK").DateTimeFormat),
                Color = "White with black stripes",
                Price = 750.0
            });
            PetRepository.AddPet(new Pet
            {
                Name = "SlowPoke",
                Type = turtle,
                Birthdate = DateTime.Parse("15-01-1982", CultureInfo.GetCultureInfo("da-DK").DateTimeFormat),
                Color = "Dark green",
                Price = 365.25
            });
            PetRepository.AddPet(new Pet
            {
                Name = "Leggy",
                Type = tarantula,
                Birthdate = DateTime.Parse("05-08-2019", CultureInfo.GetCultureInfo("da-DK").DateTimeFormat),
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
