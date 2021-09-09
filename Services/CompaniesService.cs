using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
//using _2._NTBrokersDataBase.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class CompaniesService
    {
        private readonly RealEstateEfCoreContext _context;

        public CompaniesService(RealEstateEfCoreContext context)
        {
            _context = context;
        }

        public Company GetCompany(int id)
        {
            return _context.Companies.FirstOrDefault(b => b.Id == id);
        }

        public List<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public void AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            _context.Companies.Add(addCompanyViewData.Company);

            foreach (var broker in addCompanyViewData.CompanyBrokersId)
            {
                _context.CompanyBrokers.Add(new CompanyBroker
                {
                    Company = addCompanyViewData.Company,
                    BrokerId = broker
                });
            }
            _context.SaveChanges();
        }

        public void EditCompany(EditCompanyViewModel editCompanyViewData)
        {
            _context.Companies.Update(editCompanyViewData.Company);

            List<CompanyBroker> companies = _context.CompanyBrokers.Where(c => c.CompanyId == editCompanyViewData.Company.Id).ToList();

            foreach (var company in companies)
            {
                _context.CompanyBrokers.Remove(company);
            }

            if (editCompanyViewData.CompanyBrokersIds == null)
            {
                return;
            }

            foreach (var broker in editCompanyViewData.CompanyBrokersIds)
            {
                _context.CompanyBrokers.Add(new CompanyBroker
                {
                    CompanyId = editCompanyViewData.Company.Id,
                    BrokerId = broker
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
