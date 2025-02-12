﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public async Task<IContactBook> SaveAsync(IContactBook contactBook)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var dao = new ContactBookDao(contactBook);
                dao.Id = await connection.InsertAsync(dao);

                return dao.Export();
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
        public async Task DeleteAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var sql = $"DELETE FROM ContactBook WHERE Id = {id}";
                await connection.ExecuteAsync(sql);
                
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
        public async Task<IEnumerable<IContactBook>> GetAllAsync(int skip, int take)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = "SELECT * FROM ContactBook";
                var result = await connection.QueryAsync<ContactBookDao>(query);

                return result.Skip(skip).Take(take).ToList();

            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
        public async Task<IContactBook> GetAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = $"SELECT * FROM ContactBook WHERE Id = {id}";
                var result = await connection.QueryAsync<ContactBookDao>(query);

                return result.ToList().First();
            
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IContactBook> UpdateAsync(IContactBook contactBook)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);
                var dao = new ContactBookDao(contactBook);

                await connection.UpdateAsync<ContactBookDao>(dao);

                return dao;

            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
    }

    [Table("ContactBook")]
    public class ContactBookDao : IContactBook
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactBookDao()
        {
        }

        public ContactBookDao(IContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
        }

        public IContactBook Export() => new ContactBook(Id, Name);
        
        
    }
}
