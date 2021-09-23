using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using System.Linq;

namespace _2._NTBrokersDataBase.Repo
{
    public class ApartmentsRepository
    {
        private readonly RealEstateEfCoreContext _context;

        public ApartmentsRepository(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        public void AddApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            _context.SaveChanges();
        }

        //Filtering
        public IQueryable<Apartment> Query()
        {
            return _context.Apartments;
        }
    }
}
