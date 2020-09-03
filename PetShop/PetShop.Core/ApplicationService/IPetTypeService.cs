﻿using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetTypeService
    {
        PetType CreatePetType(string type);

        bool AddPetType(PetType petType);

        List<PetType> GetAllPetTypes();

        PetType GetPetTypeByID(int ID);

        List<PetType> GetPetTypeByName(string searchTitle);

        List<PetType> GetPetTypesFilterSearch(Filter filter);

        PetType UpdatePetType(PetType type, int ID);

        bool DeletePetType(int ID);
    }
}