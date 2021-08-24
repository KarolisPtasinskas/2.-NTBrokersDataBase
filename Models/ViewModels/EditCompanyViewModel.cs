using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class EditCompanyViewModel
    {
        public CompanyModel Company { get; set; }
        public List<BrokerModel> Brokers { get; set; }
    }
}
