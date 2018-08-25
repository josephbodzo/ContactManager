using System;
using System.Collections.Generic;

namespace ContactManager.Core.Entities
{
    public class PhoneBook: IAggregateRoot<int>
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public virtual  IList<PhoneBookEntry> BookEntries { get; set; }
    }
}
