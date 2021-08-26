using _2._NTBrokersDataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class BrokersService
    {
        private readonly SqlConnection _connection;

        public BrokersService(SqlConnection connection)
        {
            _connection = connection;
        }

        //SELECT broker from DB
        public BrokerModel GetBroker(int id)
        {
            BrokerModel broker = new();

            _connection.Open();
            var command = new SqlCommand($"SELECT * FROM [dbo].[Brokers] WHERE [dbo].[Brokers].[Id] = {id}", _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {

                broker.Id = (int)reader.GetValue(0);
                broker.FirstName = (string)reader.GetValue(1);
                broker.LastName = (string)reader.GetValue(2);

            }
            _connection.Close();

            return broker;
        }


        //SELECT all brokers in DB
        public List<BrokerModel> GetAllBrokers()
        {
            List<BrokerModel> brokers = new();

            _connection.Open();
            var command = new SqlCommand("SELECT * FROM [dbo].[Brokers]", _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                BrokerModel broker = new()
                {
                    Id = (int)reader.GetValue(0),
                    FirstName = (string)reader.GetValue(1),
                    LastName = (string)reader.GetValue(2)

                };

                brokers.Add(broker);
            }
            _connection.Close();

            return brokers;
        }

        //INSERTING broker to DB
        public void AddBroker(BrokerModel broker)
        {
            _connection.Open();

            var command = new SqlCommand($"INSERT INTO dbo.Brokers (FirstName, LastName) values ('{broker.FirstName}', '{broker.LastName}')", _connection);
            var reader = command.ExecuteReader();

            _connection.Close();

        }

        //SELECT specific broker apartments from DB

        public List<ApartmentModel> GetBrokerApartments(int id)
        {
            List<ApartmentModel> apartments = new();

            _connection.Open();
            var command = new SqlCommand($"SELECT * FROM [dbo].[Apartments] WHERE [dbo].[Apartments].[BrokerId] = {id}", _connection);
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
                    CompanyId = (int)reader.GetValue(8)

                };

                apartments.Add(apartment);
            }
            _connection.Close();

            return apartments;
        }

        //SELECT all unassigned apartments which belongs to companies where broker works
        public List<ApartmentModel> GetUnassignedApartments(int id)
        {
            List<ApartmentModel> apartments = new();

            _connection.Open();
            var command = new SqlCommand($"SELECT * FROM [dbo].[Apartments] LEFT JOIN [dbo].[Companies_Brokers] ON [dbo].[Apartments].[CompanyId] = [dbo].[Companies_Brokers].[CompanyId] WHERE [dbo].[Apartments].[BrokerId] = 0 AND [dbo].[Companies_Brokers].[BrokerId] = {id}", _connection);
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
                    BrokerId = id,
                    CompanyId = (int)reader.GetValue(7)

                };

                apartments.Add(apartment);
            }
            _connection.Close();

            return apartments;
        }

        //UPDATING apartment with new BrokerId (after assigning it to broker)
        public void AssignApartment(AssignApartmentViewModel assignApartmentViewData)
        {
            foreach (var apartmentId in assignApartmentViewData.SelectedApartments)
            {
                _connection.Open();

                var command = new SqlCommand($"UPDATE [dbo].[Apartments] SET [dbo].[Apartments].[BrokerId] = {assignApartmentViewData.BrokerId} WHERE [dbo].[Apartments].[Id] = {apartmentId}", _connection);
                var reader = command.ExecuteReader();

                _connection.Close();
            }
        }


        //UPDATE apartment unassigning his BrokerID value (changint to 0)
        public void UnAssignApartment(int id)
        {
            _connection.Open();

            var command = new SqlCommand($"UPDATE [dbo].[Apartments] SET [dbo].[Apartments].[BrokerId] = 0 WHERE [dbo].[Apartments].[Id] = {id}", _connection);
            var reader = command.ExecuteReader();

            _connection.Close();
        }
    }
}
