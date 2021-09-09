using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class Company
    {
#nullable enable
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? BuildingNo { get; set; }

#nullable disable

        public ICollection<CompanyBroker> CompanyBrokers { get; set; }
    }
}
