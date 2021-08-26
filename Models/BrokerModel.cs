using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class BrokerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get 
            {
                return $"{FirstName} {LastName}";
            } }
    }
}
