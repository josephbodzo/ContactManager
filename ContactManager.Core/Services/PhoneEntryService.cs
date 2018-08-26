using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Core.Repositories;
using ContactManager.Infrastructure.Services.Constants;
using ContactManager.Infrastructure.Services.Exceptions;
using ContactManager.Infrastructure.Services.Extensions;
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
            Guard.ThrowIfLessThanMinLength(name, "Name", 5);
            Guard.ThrowIfGreaterThanMaxLength(name, "Name", 50);
            Guard.ThrowIfDefaultOrEmpty(phoneNumber, "Phone Number");
            Guard.ThrowIfDefaultValue(phoneBookId, "Phone Book");
            Guard.ThrowIfRegexNotMatch(phoneNumber, "Phone Number", Constants.CONSTANT_10_DIGIT_PHONE_FORMAT);

            var phoneBook = await _phoneBookService.GetPhoneBookAsync(phoneBookId);

            if (phoneBook == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            var phoneEntry = await GetPhoneEntryByNumberAsync(phoneNumber);
            if (phoneEntry == null)
            {
                phoneEntry = new PhoneEntry
                {
                    Name = name,
                    PhoneNumber = phoneNumber.RemoveNonNumericChars(),
                    DateCreated = _clock.Now,
                    DateModified = _clock.Now,
                };
                await _repository.AddAsync(phoneEntry);
            }
            else
            {
                phoneEntry.Name = name;
            }

            if (phoneEntry.BookEntries.All(p => p.PhoneBookId != phoneBookId))
            {
                phoneEntry.BookEntries.Add(new PhoneBookEntry
                {
                    PhoneBook = phoneBook,
                    DateCreated = _clock.Now
                });
            }

            await _repository.SaveChangesAsync();
            return phoneEntry;
        }

        public async Task EditPhoneEntryAsync(PhoneEntry phoneEntry)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneEntry.Name, nameof(phoneEntry.Name));
            Guard.ThrowIfLessThanMinLength(phoneEntry.Name, nameof(phoneEntry.Name), 5);
            Guard.ThrowIfGreaterThanMaxLength(phoneEntry.Name, nameof(phoneEntry.Name), 50);
            Guard.ThrowIfDefaultOrEmpty(phoneEntry.PhoneNumber, "Phone Number");
            Guard.ThrowIfRegexNotMatch(phoneEntry.PhoneNumber, "Phone Number", Constants.CONSTANT_10_DIGIT_PHONE_FORMAT);

            var entity = await GetPhoneEntryAsync(phoneEntry.Id);

            if (entity == null)
            {
                throw new NotFoundException("Phone entry not found");
            }

            entity.Name = phoneEntry.Name;
            entity.PhoneNumber = phoneEntry.PhoneNumber.RemoveNonNumericChars();
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

        public async Task RemovePhoneEntryAsync(int phoneBookId, int id)
        {
            var phoneEntry = await GetPhoneEntryAsync(id);

            if (phoneEntry == null)
            {
                throw new NotFoundException("Phone entry not found");
            }

            var bookEntry = phoneEntry.BookEntries.FirstOrDefault(f => f.PhoneBookId == phoneBookId);
            if (bookEntry != null)
            {
                phoneEntry.BookEntries.Remove(bookEntry);
            }
           
            if (!phoneEntry.BookEntries.Any())
            {
                _repository.Delete(phoneEntry);
            }
           
            await _repository.SaveChangesAsync();
        }

        public async Task<PhoneEntry> GetPhoneEntryAsync(int id)
        {
            Guard.ThrowIfDefaultValue(id, "Phone entry");

            var phoneEntry = await
                _repository.FindByIdAsync(id);

            return phoneEntry;
        }

        public async Task<PhoneEntry> GetPhoneEntryByNumberAsync(string phoneNumber)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneNumber, "Phone number");
            Guard.ThrowIfRegexNotMatch(phoneNumber, "Phone Number", Constants.CONSTANT_10_DIGIT_PHONE_FORMAT);

            var sanitizedNumber = phoneNumber.RemoveNonNumericChars();
            var phoneEntry = await _repository.Entities.FirstOrDefaultAsync(f => f.PhoneNumber == sanitizedNumber);

            return phoneEntry;
        }
    }
}
