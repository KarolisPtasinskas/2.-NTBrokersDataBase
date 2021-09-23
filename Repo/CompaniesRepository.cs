using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Repo
{
    public class CompaniesRepository
    {
        private readonly RealEstateEfCoreContext _context;

        public CompaniesRepository(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        public void EditCompany(Company company, List<int> companyBrokersIds)
        {
            _context.Companies.Update(company);

            List<CompanyBroker> companies = _context.CompanyBrokers.Where(c => c.CompanyId == company.Id).ToList();

            foreach (var selectedCompany in companies)
            {
                _context.CompanyBrokers.Remove(selectedCompany);
            }

            if (companyBrokersIds == null)
            {
                return;
            }

            foreach (var brokerId in companyBrokersIds)
            {
                _context.CompanyBrokers.Add(new CompanyBroker
                {
                    CompanyId = company.Id,
                    BrokerId = brokerId
                });
            }

            _context.SaveChanges();
        }

        public List<Broker> GetAllBrokersInCompany(int id)
        {
            var companyBrokers = _context.CompanyBrokers.Where(c => c.CompanyId == id).ToList();

            List<Broker> brokers = new();

            foreach (var obj in companyBrokers)
            {
                brokers.Add(_context.Brokers.FirstOrDefault(b => b.Id == obj.BrokerId));
            }

            return brokers;
        }
    }
}
