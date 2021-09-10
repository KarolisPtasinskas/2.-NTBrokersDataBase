using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
//using _2._NTBrokersDataBase.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class CompaniesService
    {
        private readonly CompaniesRepository _companiesRepository;

        public CompaniesService(CompaniesRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public Company GetCompany(int id)
        {
            return _companiesRepository.GetCompany(id);
        }

        public List<Company> GetAllCompanies()
        {
            return _companiesRepository.GetAllCompanies();
        }

        public void AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            _companiesRepository.AddCompany(addCompanyViewData);
        }

        public void EditCompany(EditCompanyViewModel editCompanyViewData)
        {
            _companiesRepository.EditCompany(editCompanyViewData);
        }

        public List<Broker> GetAllBrokersInCompany(int id)
        {
            return _companiesRepository.GetAllBrokersInCompany(id);
        }

    }
}
