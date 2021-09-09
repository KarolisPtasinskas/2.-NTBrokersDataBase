using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class BrokerApartmentsViewModel
    {
        public Broker Broker { get; set; }
        public List<Apartment> Apartments { get; set; }

    }
}
