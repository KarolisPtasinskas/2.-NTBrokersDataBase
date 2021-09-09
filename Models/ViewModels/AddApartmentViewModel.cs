using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class AddApartmentViewModel
    {
        public Apartment Apartment { get; set; }
        public List<Company> Companies { get; set; }
    }
}
