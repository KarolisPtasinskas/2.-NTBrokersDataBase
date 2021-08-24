using _2._NTBrokersDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class ViewDataService
    {
        ApartmentsService _apartmentsService;
        CompaniesService _companiesService;
        BrokersService _brokersService;

        public ViewDataService(ApartmentsService apartmentsService, CompaniesService companiesService, BrokersService brokersService)
        {
            _apartmentsService = apartmentsService;
            _companiesService = companiesService;
            _brokersService = brokersService;
        }

        public AddApartmentViewModel AddApartment()
        {
            AddApartmentViewModel addApartmentView = new()
            {
                Apartment = new ApartmentModel(),
                Companies = _companiesService.GetAllCompanies()
            };
            return addApartmentView;
        }

        public AddCompanyViewModel AddCompany()
        {
            AddCompanyViewModel addCompanyView = new()
            {
                Company = new CompanyModel(),
                Brokers = _brokersService.GetAllBrokers()
            };

            return addCompanyView;
        }

        public EditCompanyViewModel EditCompany(int id)
        {
            EditCompanyViewModel editCompanyView = new()
            {
                Company = _companiesService.GetCompany(id),
                Brokers = _brokersService.GetAllBrokers()
            };

            return editCompanyView;
        }

        public BrokerApartmentsViewModel ShowBrokerApartments(int id)
        {
            BrokerApartmentsViewModel brokerApartmentsView = new()
            {
                Broker = _brokersService.GetBroker(id),
                Apartments = _brokersService.GetBrokerApartments(id)
            };

            return brokerApartmentsView;
        }

        public AssignApartmentViewModel AssignApartmentsToBroker(int id)
        {
            AssignApartmentViewModel AssignApartmentsView = new()
            {
                Apartments = _brokersService.GetUnassignedApartments(id),
                SelectedApartments = new(),
                BrokerId = id
            };

            return AssignApartmentsView;
        }
    }
}
