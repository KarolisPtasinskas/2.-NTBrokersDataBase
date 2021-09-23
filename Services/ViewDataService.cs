﻿using _2._NTBrokersDataBase.Models;

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

        public ApartmentsIndexViewModel GetAllApartments()
        {
            ApartmentsIndexViewModel apartmentsIndexView = new()
            {
                Apartments = _apartmentsService.GetAllApartments(),
                FilterBy = new(),
                Brokers = _brokersService.GetAllBrokers(),
                Companies = _companiesService.GetAllCompanies()
            };

            return apartmentsIndexView;
        }

        public ApartmentsIndexViewModel GetFilteredApartments(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            ApartmentsIndexViewModel apartmentsIndexView = new()
            {
                Apartments = _apartmentsService.Filter(apartmentsIndexViewData),
                FilterBy = new()
                {
                    Broker = apartmentsIndexViewData.FilterBy.Broker,
                    Company = apartmentsIndexViewData.FilterBy.Company
                },
                Brokers = _brokersService.GetAllBrokers(),
                Companies = _companiesService.GetAllCompanies()
            };

            return apartmentsIndexView;
        }

        public AddApartmentViewModel AddApartment()
        {
            AddApartmentViewModel addApartmentView = new()
            {
                Apartment = new Apartment(),
                Companies = _companiesService.GetAllCompanies()
            };
            return addApartmentView;
        }

        public AddCompanyViewModel AddCompany()
        {
            AddCompanyViewModel addCompanyView = new()
            {
                Company = new Company(),
                CompanyBrokersId = new(),
                Brokers = _brokersService.GetAllBrokers()
            };

            return addCompanyView;
        }

        public EditCompanyViewModel EditCompany(int id)
        {
            EditCompanyViewModel editCompanyView = new()
            {
                Company = _companiesService.GetCompany(id),
                CompanyBrokersIds = new(),
                Brokers = _brokersService.GetAllBrokers()
            };

            foreach (var broker in _companiesService.GetAllBrokersInCompany(id))
            {
                editCompanyView.CompanyBrokersIds.Add(broker.Id);
            }

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
