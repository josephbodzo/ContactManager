using System;

namespace ContactManager.Core.Entities
{
    public class PhoneBookEntry
    {
        public int PhoneBookId { get; set; }
        public virtual PhoneBook PhoneBook { get; set; }
        public int PhoneEntryId { get; set; }
        public virtual PhoneEntry PhoneEntry { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
