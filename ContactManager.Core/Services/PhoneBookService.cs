using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Core.Repositories;
using ContactManager.Common.Exceptions;
using ContactManager.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Core.Services
{
   public  class PhoneBookService : IPhoneBookService
    {
        private readonly IRepository<PhoneBook, int> _bookRepository;
        private readonly IRepository<PhoneEntry, int> _entryRepository;
        private readonly IClock _clock;

        public PhoneBookService(IRepository<PhoneBook, int> bookRepository,
            IClock clock, IRepository<PhoneEntry, int> entryRepository)
        {
            _bookRepository = bookRepository;
            _clock = clock;
            _entryRepository = entryRepository;
        }

        public async Task<PhoneBook> CreatePhoneBookAsync(string name)
        {
            Guard.ThrowIfDefaultOrEmpty(name, "Name");
            Guard.ThrowIfLessThanMinLength(name, "Name", 5);
            Guard.ThrowIfGreaterThanMaxLength(name, "Name", 50);
             
            await ValidateAlreadyExists(name);

            var phoneBook = new PhoneBook
            {
                Name = name,
                DateCreated = _clock.Now,
                DateModified = _clock.Now
            };
            await _bookRepository.AddAsync(phoneBook);
            await _bookRepository.SaveChangesAsync();
            return phoneBook;
        }

        public async Task EditPhoneBookAsync(PhoneBook phoneBook)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneBook.Name, nameof(phoneBook.Name));
            Guard.ThrowIfLessThanMinLength(phoneBook.Name, nameof(phoneBook.Name), 5);
            Guard.ThrowIfGreaterThanMaxLength(phoneBook.Name, nameof(phoneBook.Name), 50);

            await ValidateAlreadyExists(phoneBook.Name, phoneBook.Id);

            var entity = await GetPhoneBookAsync(phoneBook.Id);


            if (entity == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            entity.Name = phoneBook.Name;
            entity.DateModified = _clock.Now;

            _bookRepository.Update(entity);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<IList<PhoneBook>> GetPhoneBooksAsync()
        {
           return await _bookRepository.Entities.Include(book => book.BookEntries).OrderBy(p => p.Name).ToListAsync();
        }

        public async Task DeletePhoneBookAsync(int id)
        {
            var phoneBook = await GetPhoneBookAsync(id);

            if (phoneBook == null)
            {
                throw new NotFoundException("Phone book not found");
            }
            //TODO: Consider eagerly loading BookEntries, downside is we are now being concerned about how the entities are persisted whilst in the business layer
            var entriesToDelete = phoneBook.BookEntries
                .Where( f=> f.PhoneEntry.BookEntries.Count == 1)
                .Select(f => f.PhoneEntry);

            _entryRepository.DeleteMany(entriesToDelete);

            _bookRepository.Delete(phoneBook);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<PhoneBook> GetPhoneBookAsync(int id)
        {
            Guard.ThrowIfDefaultValue(id, "Phone book");

            var phoneBook = await
                _bookRepository.FindByIdAsync(id);

            return phoneBook;
        }

        private async Task ValidateAlreadyExists(string name, int? phoneBookId = null)
        {
            var exists = await
                _bookRepository.Entities.AnyAsync(p => p.Name.ToLower() == name.ToLower() 
                                                   && p.Id != phoneBookId);

            if (exists)
            {
                throw new ValidateException("Phone book already exists");
            }
        }
    }
}
