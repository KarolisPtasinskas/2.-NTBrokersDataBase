using _2._NTBrokersDataBase.Models;
//using _2._NTBrokersDataBase.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Services
{
    public class CompaniesService
    {
        private readonly SqlConnection _connection;

        public CompaniesService(SqlConnection connection)
        {
            _connection = connection;
        }

        //SELECT one company from DB + add brokers 
        public CompanyModel GetCompany(int id)
        {
            CompanyModel company = new();

            _connection.Open();
            var command = new SqlCommand($"SELECT * FROM [dbo].[Companies] WHERE [Id] = {id}", _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {

                company.Id = (int)reader.GetValue(0);
                company.Name = (string)reader.GetValue(1);
                company.City = (string)reader.GetValue(2);
                company.Street = (string)reader.GetValue(3);
                company.BuildingNo = (string)reader.GetValue(4);
                company.Brokers = new();

            }
            _connection.Close();

            company = AddBrokersToCompany(company);

            return company;
        }

        public CompanyModel AddBrokersToCompany(CompanyModel company)
        {
            _connection.Open();
            var command = new SqlCommand($"SELECT [BrokerId] FROM[dbo].[Companies_Brokers] WHERE[CompanyId] = {company.Id}", _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                company.Brokers.Add((int)reader.GetValue(0));
            }
            _connection.Close();

            return company;
        }


        //SELECT all companies in DB
        public List<CompanyModel> GetAllCompanies()
        {
            List<CompanyModel> companies = new();

            _connection.Open();
            var command = new SqlCommand("SELECT * FROM [dbo].[Companies]", _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                CompanyModel company = new()
                {
                    Id = (int)reader.GetValue(0),
                    Name = (string)reader.GetValue(1),
                    City = (string)reader.GetValue(2),
                    BuildingNo = (string)reader.GetValue(3),
                };

                companies.Add(company);
            }
            _connection.Close();

            return companies;
        }

        //INSERTING company to DB
        public void AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            _connection.Open();

            var command = new SqlCommand($"INSERT INTO dbo.Companies (CompanyName, City, Street, BuildingNo) values ('{addCompanyViewData.Company.Name}', '{addCompanyViewData.Company.City}', '{addCompanyViewData.Company.Street}', '{addCompanyViewData.Company.BuildingNo}')", _connection);
            var reader = command.ExecuteReader();

            _connection.Close();

            foreach (var broker in addCompanyViewData.Company.Brokers)
            {
                _connection.Open();
                var command2 = new SqlCommand($"INSERT INTO [dbo].[Companies_Brokers] ([CompanyId], [BrokerId]) VALUES ((SELECT [Id] FROM [dbo].[Companies] WHERE [CompanyName] = '{addCompanyViewData.Company.Name}' AND [City] = '{addCompanyViewData.Company.City}' AND [Street] = '{addCompanyViewData.Company.Street}'), {broker})", _connection);
                var reader2 = command2.ExecuteReader();
                _connection.Close();
            }
        }

        //UPDATING company in DB
        public void EditCompany(EditCompanyViewModel editCompanyViewData)
        {
            _connection.Open();
            var command = new SqlCommand($"UPDATE [dbo].[Companies] SET [CompanyName] = '{editCompanyViewData.Company.Name}', [City] = '{editCompanyViewData.Company.City}', [Street] = '{editCompanyViewData.Company.Street}', [BuildingNo] = '{editCompanyViewData.Company.BuildingNo}' WHERE [dbo].[Companies].[Id] = {editCompanyViewData.Company.Id}", _connection);
            var reader = command.ExecuteReader();
            _connection.Close();

            _connection.Open();
            var command2 = new SqlCommand($"DELETE FROM [dbo].[Companies_Brokers] WHERE [CompanyId] = {editCompanyViewData.Company.Id}", _connection);
            var reader2 = command2.ExecuteReader();
            _connection.Close();

            if (editCompanyViewData.Company.Brokers == null)
            {
                return;
            }

            foreach (var broker in editCompanyViewData.Company.Brokers)
            {
                _connection.Open();
                var command3 = new SqlCommand($"INSERT INTO [dbo].[Companies_Brokers] ([CompanyId], [BrokerId]) VALUES ({editCompanyViewData.Company.Id}, {broker})", _connection);
                var reader3 = command3.ExecuteReader();
                _connection.Close();
            }
        }

        //SELECT all brokers in specified company from DB
        public List<BrokerModel> GetAllBrokersInCompany(int id)
        {
            List<BrokerModel> brokers = new();

            _connection.Open();
            var command = new SqlCommand($"SELECT [Id], [FirstName], [LastName] FROM [dbo].[Brokers] LEFT JOIN[dbo].[Companies_Brokers] ON [dbo].[Companies_Brokers].[BrokerId] = [dbo].[Brokers].[Id] WHERE [dbo].[Companies_Brokers].[CompanyId] = {id}", _connection);
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

    }
}
