using Microsoft.EntityFrameworkCore;

namespace _2._NTBrokersDataBase.Models
{
    [Keyless]
    public class CompanyBroker
    {
        public int CompanyId { get; set; }
        public int BrokerId { get; set; }

        public Broker Broker { get; set; }
        public Company Company { get; set; }
    }


}