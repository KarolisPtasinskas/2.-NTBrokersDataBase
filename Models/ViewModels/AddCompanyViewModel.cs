using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Models
{
    public class AddCompanyViewModel
    {
        public Company Company { get; set; }
        public List<int> CompanyBrokersId { get; set; }
        public List<Broker> Brokers { get; set; }
    }
}
