using System;

namespace ContactManager.Core.Entities
{
    public class PhoneBookEntry
    {
        public int PhoneBookId { get; set; }
        public PhoneBook PhoneBook { get; set; }
        public int PhoneEntryId { get; set; }
        public PhoneEntry PhoneEntry { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
