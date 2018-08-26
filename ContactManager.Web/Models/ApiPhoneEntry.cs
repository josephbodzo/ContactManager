using System;

namespace ContactManager.Web.Models
{
    public class ApiPhoneBookEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
