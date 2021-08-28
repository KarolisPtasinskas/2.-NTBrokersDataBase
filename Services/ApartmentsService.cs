using _2._NTBrokersDataBase.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class ApartmentsService
    {
        private readonly IConfiguration _configuration;

        public ApartmentsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //SELECT all apartments in DB
        public List<ApartmentModel> GetAllApartments()
        {
            List<ApartmentModel> apartments = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                apartments = connection.Query<ApartmentModel>("SELECT *, CONCAT_WS(' ', ((SELECT ISNULL((SELECT [FirstName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), ''))), ((SELECT ISNULL((SELECT [LastName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')))) AS BrokerName, (SELECT [CompanyName] FROM [dbo].[Companies] WHERE [dbo].[Apartments].[CompanyId] = [dbo].[Companies].[Id]) AS CompanyName FROM [dbo].[Apartments]").ToList();
            }

            return apartments;
        }

        //INSERTING one apartment to DB
        public void AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            string query = $"INSERT INTO dbo.Apartments (City, Street, BuildingNo, AtFloor, FloorsInBuilding, ApartmentSpace, BrokerId, CompanyId) values ('{addApartmentViewData.Apartment.City}', '{addApartmentViewData.Apartment.Street}', '{addApartmentViewData.Apartment.BuildingNo}', {addApartmentViewData.Apartment.AtFloor}, {addApartmentViewData.Apartment.FloorsInBuilding}, {addApartmentViewData.Apartment.ApartmentSpace}, {addApartmentViewData.Apartment.BrokerId}, {addApartmentViewData.Apartment.CompanyId})";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute(query);
            }
        }
    }
}
