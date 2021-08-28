using _2._NTBrokersDataBase.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class FilterService
    {
        private readonly IConfiguration _configuration;

        public FilterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //SELECTING filtered apartments by company or broker or both.
        public List<ApartmentModel> GetApartments(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            
            var stringHelper = (apartmentsIndexViewData.FilterBy.Company != null && apartmentsIndexViewData.FilterBy.Broker != null) ? " AND " : "";
            var companyString = (apartmentsIndexViewData.FilterBy.Company != null) ? $"[dbo].[Apartments].[CompanyId] = {apartmentsIndexViewData.FilterBy.Company}" : ""; ;
            var brokerString = (apartmentsIndexViewData.FilterBy.Broker != null) ? $"[dbo].[Apartments].[BrokerId] = {apartmentsIndexViewData.FilterBy.Broker}" : ""; ;
            var fullWhereString = (apartmentsIndexViewData.FilterBy.Company != null || apartmentsIndexViewData.FilterBy.Broker != null) ? $"WHERE {companyString} {stringHelper} {brokerString}" : "";

            List<ApartmentModel> apartments = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                apartments = connection.Query<ApartmentModel>($"SELECT *, (SELECT ISNULL((SELECT [FirstName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerName, (SELECT ISNULL((SELECT [LastName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerLastName, (SELECT [CompanyName] FROM [dbo].[Companies] WHERE [dbo].[Apartments].[CompanyId] = [dbo].[Companies].[Id]) AS CompanyName  FROM [dbo].[Apartments] {fullWhereString}").ToList();
            }

            return apartments;
        }
    }
}
