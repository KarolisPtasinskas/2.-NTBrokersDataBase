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
    public class BrokersService
    {
        private readonly IConfiguration _configuration;

        public BrokersService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //SELECT broker from DB
        public BrokerModel GetBroker(int id)
        {
            BrokerModel broker = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                broker = connection.QuerySingle<BrokerModel>($"SELECT * FROM [dbo].[Brokers] WHERE [dbo].[Brokers].[Id] = {id}");
            }

            return broker;
        }


        //SELECT all brokers in DB
        public List<BrokerModel> GetAllBrokers()
        {
            List<BrokerModel> brokers = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                brokers = connection.Query<BrokerModel>("SELECT * FROM [dbo].[Brokers]").ToList();
            }

            return brokers;
        }

        //INSERTING broker to DB
        public void AddBroker(BrokerModel broker)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute($"INSERT INTO dbo.Brokers (FirstName, LastName) values ('{broker.FirstName}', '{broker.LastName}')");
            }


        }

        //SELECT specific broker apartments from DB

        public List<ApartmentModel> GetBrokerApartments(int id)
        {
            List<ApartmentModel> apartments = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                apartments = connection.Query<ApartmentModel>($"SELECT *, null AS BrokerName, null AS BrokerCompany FROM [dbo].[Apartments] WHERE [dbo].[Apartments].[BrokerId] = {id}").ToList();
            }

            return apartments;
        }

        //SELECT all unassigned apartments which belongs to companies where broker works
        public List<ApartmentModel> GetUnassignedApartments(int id)
        {
            List<ApartmentModel> apartments = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                apartments = connection.Query<ApartmentModel>($"SELECT * FROM [dbo].[Apartments] LEFT JOIN [dbo].[Companies_Brokers] ON [dbo].[Apartments].[CompanyId] = [dbo].[Companies_Brokers].[CompanyId] WHERE [dbo].[Apartments].[BrokerId] = 0 AND [dbo].[Companies_Brokers].[BrokerId] = {id}").ToList();
            }

            return apartments;
        }

        //UPDATING apartment with new BrokerId (after assigning it to broker)
        public void AssignApartment(AssignApartmentViewModel assignApartmentViewData)
        {
            foreach (var apartmentId in assignApartmentViewData.SelectedApartments)
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute($"UPDATE [dbo].[Apartments] SET [dbo].[Apartments].[BrokerId] = {assignApartmentViewData.BrokerId} WHERE [dbo].[Apartments].[Id] = {apartmentId}");
                }

            }
        }


        //UPDATE apartment unassigning his BrokerID value (changint to 0)
        public void UnAssignApartment(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute($"UPDATE [dbo].[Apartments] SET [dbo].[Apartments].[BrokerId] = 0 WHERE [dbo].[Apartments].[Id] = {id}");
            }

        }
    }
}
