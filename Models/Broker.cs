using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Models
{
    public class Broker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<CompanyBroker> CompanyBrokers { get; set; }
    }
}