using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Repo;
using System.Collections.Generic;

namespace _2._NTBrokersDataBase.Services
{
    public class ApartmentsService
    {
        private readonly ApartmentsRepository _apartmentsRepository;

        public ApartmentsService(ApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }

        public Apartment GetOneApartment(int id)
        {
            return _apartmentsRepository.GetOneApartment(id);
        }

        public List<Apartment> GetAllApartments()
        {
            return _apartmentsRepository.GetAllApartments();
        }

        public void AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            _apartmentsRepository.AddApartment(addApartmentViewData);
        }
    }
}
