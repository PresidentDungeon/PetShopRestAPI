using PetShop.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public class PetType: ISearchAble
    {
        public int ID { get; set; }
        public string Type { get; set; }

        public string searchValue()
        {
            return Type;
        }
    }
}
