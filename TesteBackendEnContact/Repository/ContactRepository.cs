using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task DeleteAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var sql = $"DELETE FROM Contact WHERE Id = {id}";
                await connection.ExecuteAsync(sql);
                
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IEnumerable<IContact>> GetAllAsync()
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = $"SELECT * FROM Contact";
                var result = await connection.QueryAsync<ContactDao>(query);

                return result.ToList();
            
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IContact> GetAsync(int id)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var query = $"SELECT * FROM Contact WHERE Id = {id}";
                var result = await connection.QueryAsync<ContactDao>(query);

                return result.ToList().First();
            
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IContact> SaveAsync(IContact contact)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var dao = new ContactDao(contact);
                dao.Id = await connection.InsertAsync(dao);

                return dao.Export();
            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }

        public async Task<IContact> UpdateAsync(IContact contact)
        {
            try{
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);
                var dao = new ContactDao(contact);

                await connection.UpdateAsync<ContactDao>(dao);

                return dao;

            }catch(SqliteException err){
                throw new System.Exception(err.ToString());
            }
        }
    }

    [Table("Contact")]
    public class ContactDao : IContact
    {
        [Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public int CompanyId { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public ContactDao(){}

        public ContactDao(IContact contact){
            Id = contact.Id;
            Name = contact.Name; 
            ContactBookId = contact.ContactBookId;
            CompanyId = contact.CompanyId;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;
        }

        public IContact Export() => new Contact(Id, Name, ContactBookId, CompanyId, Phone, Email, Address);
    }
}
