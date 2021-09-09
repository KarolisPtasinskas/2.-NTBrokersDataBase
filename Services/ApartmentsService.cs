using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class ApartmentsService
    {
        private readonly RealEstateEfCoreContext _context;

        public ApartmentsService(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        public Apartment GetOneApartment(int id)
        {
            return _context.Apartments.FirstOrDefault(a => a.Id == id);
        }

        public List<Apartment> GetAllApartments()
        {
            return _context.Apartments.ToList();
        }

        public void AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            _context.Apartments.Add(addApartmentViewData.Apartment);
            _context.SaveChanges();
        }
    }
}
