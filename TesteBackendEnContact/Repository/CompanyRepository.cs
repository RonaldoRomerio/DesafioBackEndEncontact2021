﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public CompanyRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ICompany> SaveAsync(ICompany company)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);
                var dao = new CompanyDao(company);

                dao.Id = await connection.InsertAsync<CompanyDao>(dao);

                return dao;
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task DeleteAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);
                connection.Open();

                using var transaction = connection.BeginTransaction();

                var sql = new StringBuilder();
                sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
                sql.AppendLine("UPDATE Contact SET CompanyId = null WHERE CompanyId = @id;");

                await connection.ExecuteAsync(sql.ToString(), new {id}, transaction);

                transaction.Commit();
                connection.Close();
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IEnumerable<ICompany>> GetAllAsync()
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = "SELECT * FROM Company";
                var result = await connection.QueryAsync<CompanyDao>(query);

                return result.ToList();
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<ICompany> GetAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = $"SELECT * FROM Company where Id = @id";
                var result = await connection.QuerySingleOrDefaultAsync<CompanyDao>(query, new {id});

                return result;
            
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<ICompany> UpdateAsync(ICompany company)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);
                var dao = new CompanyDao(company);

                await connection.UpdateAsync<CompanyDao>(dao);

                return dao;

            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
    }

    [Table("Company")]
    public class CompanyDao : ICompany
    {
        [Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public CompanyDao()
        {
        }

        public CompanyDao(ICompany company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }

        public ICompany Export() => new Company(Id, ContactBookId, Name);
    }
}
