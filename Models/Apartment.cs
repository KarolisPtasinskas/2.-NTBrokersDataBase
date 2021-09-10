using System.ComponentModel.DataAnnotations;

namespace _2._NTBrokersDataBase.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string BuildingNo { get; set; }

        [Required]
        public int? AtFloor { get; set; }

        [Required]
        public int FloorsInBuilding { get; set; }

        [Required]
        public int ApartmentSpace { get; set; }

        public int? BrokerId { get; set; }

        [Required]
        public int CompanyId { get; set; }


        public Company Company { get; set; }
    }
}
