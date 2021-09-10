using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Services
{
    public class BrokersService
    {
        private readonly BrokersRepository _brokerRepository;

        public BrokersService(BrokersRepository brokerRepository)
        {
            _brokerRepository = brokerRepository;
        }

        public Broker GetBroker(int id)
        {
            return _brokerRepository.GetBroker(id);
        }

        public List<Broker> GetAllBrokers()
        {
            return _brokerRepository.GetAllBrokers();
        }

        public void AddBroker(Broker broker)
        {
            _brokerRepository.AddBroker(broker);
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
            _brokerRepository.AssignApartment(assignApartmentViewData);
        }

        public void UnAssignApartment(int id)
        {
            _brokerRepository.UnAssignApartment(id);
        }


    }
}
