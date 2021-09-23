using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
using _2._NTBrokersDataBase.Repo.RepositoryUsingEFinMVC.GenericRepository;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Services
{
    public class ApartmentsService
    {
        private readonly ApartmentsRepository _apartmentsRepository;
        private readonly IGenericRepository<Apartment> _repository;

        public ApartmentsService(ApartmentsRepository apartmentsRepository, IGenericRepository<Apartment> repository)
        {
            _apartmentsRepository = apartmentsRepository;
            _repository = repository;
        }

        public Apartment GetOneApartment(int id)
        {
            return _repository.GetById(id);
        }

        public List<Apartment> GetAllApartments()
        {
            return (List<Apartment>)_repository.GetAll();
        }

        public void AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            _repository.Insert(addApartmentViewData.Apartment);
            _repository.Save();
        }

        public List<Apartment> Filter(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            return _apartmentsRepository.Query().Where(a => a.CompanyId == apartmentsIndexViewData.FilterBy.Company || a.BrokerId == apartmentsIndexViewData.FilterBy.Broker).ToList();
        }
    }
}
