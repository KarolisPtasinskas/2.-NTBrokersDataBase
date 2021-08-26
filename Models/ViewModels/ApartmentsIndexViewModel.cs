using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class ApartmentsIndexViewModel
    {
        public FilterModel FilterBy { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public List<BrokerModel> Brokers { get; set; }
        public List<CompanyModel> Companies { get; set; }
    }
}
