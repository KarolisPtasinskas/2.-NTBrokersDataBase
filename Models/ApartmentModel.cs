using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Models
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public int? AtFloor { get; set; }
        public int? FloorsInBuilding { get; set; }
        public int? ApartmentSpace { get; set; }
        public int BrokerId { get; set; }
        public int CompanyId { get; set; }

        public string BrokerName { get; set; }
        public string CompanyName { get; set; }
        public string Name
        {
            get
            {
                return $"{Street} g. {BuildingNo}, {City}";
            }
        }
    }
}
