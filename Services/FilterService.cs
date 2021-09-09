using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class FilterService
    {
        private readonly RealEstateEfCoreContext _context;

        public FilterService(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        //SELECTING filtered apartments by company or broker or both.
        public List<Apartment> GetApartments(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            List<Apartment> apartments = _context.Apartments.Where(a => a.CompanyId == apartmentsIndexViewData.FilterBy.Company || a.BrokerId == apartmentsIndexViewData.FilterBy.Broker).ToList();

            return apartments;
        }
    }
}
