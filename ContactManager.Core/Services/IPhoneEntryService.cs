using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Core.Entities;

namespace ContactManager.Core.Services
{
    public interface IPhoneEntryService
    {
        Task<PhoneEntry> CreatePhoneEntryAsync(string name, string phoneNumber, int phoneBookId);
        Task RemovePhoneEntryAsync(int phoneBookId, int id);
        Task EditPhoneEntryAsync(PhoneEntry phoneEntry);
        Task<IList<PhoneEntry>> GetPhoneEntriesAsync(int phoneBookId);
        Task<PhoneEntry> GetPhoneEntryAsync(int id);
        Task<PhoneEntry> GetPhoneEntryByNumberAsync(string phoneNumber);
    }
}