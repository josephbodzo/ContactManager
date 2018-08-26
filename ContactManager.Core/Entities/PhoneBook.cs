using System;
using System.Collections.Generic;

namespace ContactManager.Core.Entities
{
    public class PhoneBook: IAggregateRoot<int>
    {
        public PhoneBook()
        {
        }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        //TODO: This is inefficient, need to get the count once when we load book from persistance
        public int ContactCount => BookEntries.Count;
        public virtual  IList<PhoneBookEntry> BookEntries { get; set; } = new List<PhoneBookEntry>();
    }
}
