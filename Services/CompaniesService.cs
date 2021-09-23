using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
using _2._NTBrokersDataBase.Repo.RepositoryUsingEFinMVC.GenericRepository;
//using _2._NTBrokersDataBase.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class CompaniesService
    {
        private readonly CompaniesRepository _companiesRepository;
        private readonly IGenericRepository<Company> _repository;
        private readonly IGenericRepository<CompanyBroker> _cbRepository;

        public CompaniesService(CompaniesRepository companiesRepository, IGenericRepository<Company> repository, IGenericRepository<CompanyBroker> cbRepository)
        {
            _companiesRepository = companiesRepository;
            _repository = repository;
            _cbRepository = cbRepository;
        }

        public Company GetCompany(int id)
        {
            return _repository.GetById(id);
        }

        public List<Company> GetAllCompanies()
        {
            return (List<Company>)_repository.GetAll();
        }

        public void AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            _repository.Insert(addCompanyViewData.Company);
            _repository.Save();

            foreach (var broker in addCompanyViewData.CompanyBrokersId)
            {
                _cbRepository.Insert(new CompanyBroker
                {
                    CompanyId = addCompanyViewData.Company.Id,
                    BrokerId = broker
                });
            }

            _cbRepository.Save();
        }

        public void EditCompany(EditCompanyViewModel editCompanyViewData)
        {
            _companiesRepository.EditCompany(editCompanyViewData.Company, editCompanyViewData.CompanyBrokersIds);
        }

        public List<Broker> GetAllBrokersInCompany(int id)
        {
            return _companiesRepository.GetAllBrokersInCompany(id);
        }

    }
}
