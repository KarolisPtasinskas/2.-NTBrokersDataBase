using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class AssignApartmentViewModel
    {
        public List<Apartment> Apartments { get; set; }
        public List<int> SelectedApartments { get; set; }
        public int BrokerId { get; set; }
    }
}
