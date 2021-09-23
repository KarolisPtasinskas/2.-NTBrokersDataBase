using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
using _2._NTBrokersDataBase.Repo.RepositoryUsingEFinMVC.GenericRepository;
using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Services
{
    public class BrokersService
    {
        private readonly BrokersRepository _brokerRepository;
        private readonly IGenericRepository<Broker> _repository;

        public BrokersService(BrokersRepository brokerRepository, IGenericRepository<Broker> repository)
        {
            _brokerRepository = brokerRepository;
            _repository = repository;
        }

        public Broker GetBroker(int id)
        {
            return _repository.GetById(id);
        }

        public List<Broker> GetAllBrokers()
        {
            return (List<Broker>)_repository.GetAll();
        }

        public void AddBroker(Broker broker)
        {
            _repository.Insert(broker);
            _repository.Save();
        }

        public List<Apartment> GetBrokerApartments(int id)
        {
            return _brokerRepository.GetBrokerApartments(id);
        }

        public List<Apartment> GetUnassignedApartments(int id)
        {
            return _brokerRepository.GetUnassignedApartments(id);
        }

        public void AssignApartment(AssignApartmentViewModel assignApartmentViewData)
        {
            _brokerRepository.AssignApartment(assignApartmentViewData.SelectedApartments, assignApartmentViewData.BrokerId);
        }

        public void UnAssignApartment(int id)
        {
            _brokerRepository.UnAssignApartment(id);
        }


    }
}
