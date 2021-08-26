using _2._NTBrokersDataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class FilterService
    {
        private readonly SqlConnection _connection;

        public FilterService(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<ApartmentModel> GetApartments(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            
            var stringHelper = (apartmentsIndexViewData.FilterBy.Company != null && apartmentsIndexViewData.FilterBy.Broker != null) ? " AND " : "";
            var companyString = (apartmentsIndexViewData.FilterBy.Company != null) ? $"[dbo].[Apartments].[CompanyId] = {apartmentsIndexViewData.FilterBy.Company}" : ""; ;
            var brokerString = (apartmentsIndexViewData.FilterBy.Broker != null) ? $"[dbo].[Apartments].[BrokerId] = {apartmentsIndexViewData.FilterBy.Broker}" : ""; ;
            var fullWhereString = (apartmentsIndexViewData.FilterBy.Company != null || apartmentsIndexViewData.FilterBy.Broker != null) ? $"WHERE {companyString} {stringHelper} {brokerString}" : "";


                List<ApartmentModel> apartments = new();

                _connection.Open();
                var command = new SqlCommand($"SELECT *, (SELECT ISNULL((SELECT [FirstName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerName, (SELECT ISNULL((SELECT [LastName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerLastName, (SELECT [CompanyName] FROM [dbo].[Companies] WHERE [dbo].[Apartments].[CompanyId] = [dbo].[Companies].[Id]) FROM [dbo].[Apartments] {fullWhereString}", _connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApartmentModel apartment = new()
                    {
                        Id = (int)reader.GetValue(0),
                        City = (string)reader.GetValue(1),
                        Street = (string)reader.GetValue(2),
                        BuildingNo = (string)reader.GetValue(3),
                        Floor = (int)reader.GetValue(4),
                        FloorsInBuilding = (int)reader.GetValue(5),
                        Space = (int)reader.GetValue(6),
                        BrokerId = (int)reader.GetValue(7),
                        CompanyId = (int)reader.GetValue(8),
                        BrokerName = $"{(string)reader.GetValue(9)} {(string)reader.GetValue(10)}",
                        CompanyName = (string)reader.GetValue(11)
                    };

                    apartments.Add(apartment);
                }
                _connection.Close();

                return apartments;
        }
    }
}
