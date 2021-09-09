using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Models
{
    public class EditCompanyViewModel
    {
        public Company Company { get; set; }
        public List<int> CompanyBrokersIds { get; set; }
        public List<Broker> Brokers { get; set; }
    }
}
