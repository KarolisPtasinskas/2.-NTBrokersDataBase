using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Repo
{
    public class BrokersRepository
    {
        private readonly RealEstateEfCoreContext _context;

        public BrokersRepository(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        public void AddBroker(Broker broker)
        {
            _context.Brokers.Add(broker);

            _context.SaveChanges();
        }

        public List<Apartment> GetBrokerApartments(int id)
        {
            return _context.Apartments.Where(a => a.BrokerId == id).ToList();
        }

        public List<Apartment> GetUnassignedApartments(int id)
        {
            var company = _context.CompanyBrokers.FirstOrDefault(c => c.BrokerId == id);

            if (company == null)
            {
                return new List<Apartment>();
            }

            return _context.Apartments.Where(a => a.BrokerId == null && a.CompanyId == company.CompanyId).ToList();
        }

        public void AssignApartment(List<int> selectedApartments, int brokerId)
        {
            foreach (var apartmentId in selectedApartments)
            {
                var apartment = _context.Apartments.FirstOrDefault(a => a.Id == apartmentId);
                apartment.BrokerId = brokerId;
                _context.Apartments.Update(apartment);
            }
            _context.SaveChanges();
        }

        public void UnAssignApartment(int id)
        {
            var apartment = _context.Apartments.FirstOrDefault(a => a.Id == id);
            apartment.BrokerId = null;
            _context.Apartments.Update(apartment);
            _context.SaveChanges();
        }


    }
}
