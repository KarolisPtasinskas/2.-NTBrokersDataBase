using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class AddApartmentViewModel
    {
        public ApartmentModel Apartment { get; set; }
        public List<CompanyModel> Companies { get; set; }
    }
}
