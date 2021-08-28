using _2._NTBrokersDataBase.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public CompaniesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //SELECT one company from DB + add brokers 
        public CompanyModel GetCompany(int id)
        {
            CompanyModel company = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                company = connection.QuerySingle<CompanyModel>($"SELECT * FROM [dbo].[Companies] WHERE [Id] = {id}");
            }

            company = AddBrokersToCompany(company);

            return company;
        }

        public CompanyModel AddBrokersToCompany(CompanyModel company)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                company.Brokers = connection.Query<int>($"SELECT [BrokerId] FROM[dbo].[Companies_Brokers] WHERE[CompanyId] = {company.Id}").ToList();
            }

            return company;
        }


        //SELECT all companies in DB
        public List<CompanyModel> GetAllCompanies()
        {
            List<CompanyModel> companies = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                companies = connection.Query<CompanyModel>("SELECT * FROM [dbo].[Companies]").ToList();
            }

            return companies;
        }

        //INSERTING company to DB
        public void AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute($"INSERT INTO dbo.Companies (CompanyName, City, Street, BuildingNo) values ('{addCompanyViewData.Company.CompanyName}', '{addCompanyViewData.Company.City}', '{addCompanyViewData.Company.Street}', '{addCompanyViewData.Company.BuildingNo}')");
            }

            foreach (var broker in addCompanyViewData.Company.Brokers)
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute($"INSERT INTO [dbo].[Companies_Brokers] ([CompanyId], [BrokerId]) VALUES ((SELECT [Id] FROM [dbo].[Companies] WHERE [CompanyName] = '{addCompanyViewData.Company.CompanyName}' AND [City] = '{addCompanyViewData.Company.City}' AND [Street] = '{addCompanyViewData.Company.Street}'), {broker})");
                }
            }
        }

        //UPDATING company in DB
        public void EditCompany(EditCompanyViewModel editCompanyViewData)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute($"UPDATE [dbo].[Companies] SET [CompanyName] = '{editCompanyViewData.Company.CompanyName}', [City] = '{editCompanyViewData.Company.City}', [Street] = '{editCompanyViewData.Company.Street}', [BuildingNo] = '{editCompanyViewData.Company.BuildingNo}' WHERE [dbo].[Companies].[Id] = {editCompanyViewData.Company.Id}");
            }

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Execute($"DELETE FROM [dbo].[Companies_Brokers] WHERE [CompanyId] = {editCompanyViewData.Company.Id}");
            }

            if (editCompanyViewData.Company.Brokers == null)
            {
                return;
            }

            foreach (var broker in editCompanyViewData.Company.Brokers)
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute($"INSERT INTO [dbo].[Companies_Brokers] ([CompanyId], [BrokerId]) VALUES ({editCompanyViewData.Company.Id}, {broker})");
                }
            }
        }

        //SELECT all brokers in specified company from DB
        public List<BrokerModel> GetAllBrokersInCompany(int id)
        {
            List<BrokerModel> brokers = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                brokers = connection.Query<BrokerModel>($"SELECT [Id], [FirstName], [LastName] FROM [dbo].[Brokers] LEFT JOIN[dbo].[Companies_Brokers] ON [dbo].[Companies_Brokers].[BrokerId] = [dbo].[Brokers].[Id] WHERE [dbo].[Companies_Brokers].[CompanyId] = {id}").ToList();
            }

            return brokers;
        }

    }
}
