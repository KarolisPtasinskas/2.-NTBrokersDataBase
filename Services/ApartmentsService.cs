using _2._NTBrokersDataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class ApartmentsService
    {
        private readonly SqlConnection _connection;

        public ApartmentsService(SqlConnection connection)
        {
            _connection = connection;
        }

        //SELECT all apartments in DB
        public List<ApartmentModel> GetAllApartments()
        {
            List<ApartmentModel> apartments = new();

            _connection.Open();
            var command = new SqlCommand("SELECT *, (SELECT ISNULL((SELECT [FirstName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerName, (SELECT ISNULL((SELECT [LastName] FROM [dbo].[Brokers] WHERE [dbo].[Apartments].[BrokerId] = [dbo].[Brokers].[Id]), '')) AS BrokerLastName, (SELECT [CompanyName] FROM [dbo].[Companies] WHERE [dbo].[Apartments].[CompanyId] = [dbo].[Companies].[Id]) FROM [dbo].[Apartments]", _connection);
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

        //INSERTING one apartment to DB
        public void AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            _connection.Open();

            var command = new SqlCommand($"INSERT INTO dbo.Apartments (City, Street, BuildingNo, AtFloor, FloorsInBuilding, ApartmentSpace, BrokerId, CompanyId) values ('{addApartmentViewData.Apartment.City}', '{addApartmentViewData.Apartment.Street}', '{addApartmentViewData.Apartment.BuildingNo}', {addApartmentViewData.Apartment.Floor}, {addApartmentViewData.Apartment.FloorsInBuilding}, {addApartmentViewData.Apartment.Space}, {addApartmentViewData.Apartment.BrokerId}, {addApartmentViewData.Apartment.CompanyId})", _connection);
            var reader = command.ExecuteReader();

            _connection.Close();
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
    }
}
