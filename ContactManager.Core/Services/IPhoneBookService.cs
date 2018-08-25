using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Services.Enums;
using ContactManager.Infrastructure.Services.Paging;
using ContactManager.Infrastructure.Services.Utilities;

namespace ContactManager.Core.Services
{
    public interface IPhoneBookService
    {
        Task<PhoneBook> CreatePhoneBookAsync(string name);
        Task EditPhoneBook(PhoneBook phoneBook);
        Task<PhoneBook> GetPhoneBook(int id);
        Task DeletePhoneBook(int id);

        PagedList<PhoneBook> SearchPhoneBooks(string searchValue, PagingOptions pagingOptions);
    }
}