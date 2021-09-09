using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Models
{
    public class ApartmentsIndexViewModel
    {
        public FilterModel FilterBy { get; set; }
        public List<Apartment> Apartments { get; set; }
        public List<Broker> Brokers { get; set; }
        public List<Company> Companies { get; set; }
    }
}
