using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class BrokerApartmentsViewModel
    {
        public BrokerModel Broker { get; set; }
        public List<ApartmentModel> Apartments { get; set; }

    }
}
