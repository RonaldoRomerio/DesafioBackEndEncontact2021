using System.ComponentModel.DataAnnotations;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Contact
{
    public class Contact : IContact
    {
        [Key]
        public int Id { get;  set; }
        [Required]
        public int ContactBookId { get;  set; }
        [Required]
        public int CompanyId { get;  set; }
        [Required]
        public string Name { get;  set; }
        [Required]
        public string Phone { get;  set; }
        public string Email { get;  set; }
        public string Address { get;  set; }

        public Contact(int Id, string Name, int ContactBookId, int CompanyId, string Phone, string Email, string Address)
        {
            this.Id = Id;
            this.ContactBookId = ContactBookId;
            this.CompanyId = CompanyId;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
        }
    }
}
