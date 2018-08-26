using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Core.Repositories;
using ContactManager.Infrastructure.Services.Exceptions;
using ContactManager.Infrastructure.Services.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Core.Services
{
   public  class PhoneEntryService : IPhoneEntryService
    {
        private readonly IRepository<PhoneEntry, int> _repository;
        private readonly IClock _clock;
        private readonly IPhoneBookService _phoneBookService;

        public PhoneEntryService(IRepository<PhoneEntry, int> repository, IClock clock, IPhoneBookService phoneBookService)
        {
            _repository = repository;
            _clock = clock;
            _phoneBookService = phoneBookService;
        }

        public async Task<PhoneEntry> CreatePhoneEntryAsync(string name, string phoneNumber, int phoneBookId)
        {
            Guard.ThrowIfDefaultOrEmpty(name, "Name");
            Guard.ThrowIfDefaultOrEmpty(phoneNumber, "Phone Number");
            Guard.ThrowIfDefaultValue(phoneBookId, "Phone Book");

            var phoneBook = await _phoneBookService.GetPhoneBookAsync(phoneBookId);

            if (phoneBook == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            var phoneEntry = await GetPhoneEntryByNumberAsync(phoneNumber) ?? 
            new PhoneEntry
            {
                Name = name,
                PhoneNumber = phoneNumber,
                DateCreated = _clock.Now,
                DateModified = _clock.Now,
            };

            phoneEntry.BookEntries.Add(new PhoneBookEntry
            {
                PhoneBook = phoneBook,
                DateCreated =  _clock.Now
            });

            await _repository.AddAsync(phoneEntry);
            await _repository.SaveChangesAsync();
            return phoneEntry;
        }

        public async Task EditPhoneEntryAsync(PhoneEntry phoneEntry)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneEntry.Name, "Name");
            Guard.ThrowIfDefaultOrEmpty(phoneEntry.PhoneNumber, "Phone Number");

            var entity = await GetPhoneEntryAsync(phoneEntry.Id);

            if (entity == null)
            {
                throw new NotFoundException("Phone entry not found");
            }

            entity.Name = phoneEntry.Name;
            entity.PhoneNumber = phoneEntry.PhoneNumber;
            entity.DateModified = _clock.Now;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<IList<PhoneEntry>> GetPhoneEntriesAsync(int phoneBookId)
        {
            var phoneBook = await _phoneBookService.GetPhoneBookAsync(phoneBookId);

            if (phoneBook == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            return phoneBook.BookEntries.Select(f=> f.PhoneEntry).ToList();
        }

        public async Task DeletePhoneEntryAsync(int id)
        {
            var entity = await GetPhoneEntryAsync(id);

            if (entity == null)
            {
                throw new NotFoundException("Phone entry not found");
            }

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<PhoneEntry> GetPhoneEntryAsync(int id)
        {
            Guard.ThrowIfDefaultValue(id, "Phone entry");

            var phoneEntry = await
                _repository.FindByIdAsync(id);

            return phoneEntry;
        }

        private async Task<PhoneEntry> GetPhoneEntryByNumberAsync(string phoneNumber)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneNumber, "Phone number");

            var phoneEntry = await _repository.Entities.FirstOrDefaultAsync(f => f.PhoneNumber == phoneNumber);

            return phoneEntry;
        }
    }
}
