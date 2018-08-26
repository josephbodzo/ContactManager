using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Core.Entities;

namespace ContactManager.Core.Services
{
    public interface IPhoneBookService
    {
        Task<PhoneBook> CreatePhoneBookAsync(string name);
        Task EditPhoneBookAsync(PhoneBook phoneBook);
        Task<PhoneBook> GetPhoneBookAsync(int id);
        Task DeletePhoneBookAsync(int id);
        Task<IList<PhoneBook>> GetPhoneBooksAsync();
    }
}