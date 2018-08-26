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
   public  class PhoneBookService : IPhoneBookService
    {
        private readonly IRepository<PhoneBook, int> _repository;
        private readonly IClock _clock;

        public PhoneBookService(IRepository<PhoneBook, int> repository, IClock clock)
        {
            _repository = repository;
            _clock = clock;
        }

        public async Task<PhoneBook> CreatePhoneBookAsync(string name)
        {
            Guard.ThrowIfDefaultOrEmpty(name, "Name");
            await ValidateAlreadyExists(name);

            var phoneBook = new PhoneBook
            {
                Name = name,
                DateCreated = _clock.Now,
                DateModified = _clock.Now
            };
            await _repository.AddAsync(phoneBook);
            await _repository.SaveChangesAsync();
            return phoneBook;
        }

        public async Task EditPhoneBookAsync(PhoneBook phoneBook)
        {
            Guard.ThrowIfDefaultOrEmpty(phoneBook.Name, nameof(phoneBook.Name));
            await ValidateAlreadyExists(phoneBook.Name, phoneBook.Id);

            var entity = await GetPhoneBookAsync(phoneBook.Id);


            if (entity == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            entity.Name = phoneBook.Name;
            entity.DateModified = _clock.Now;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<IList<PhoneBook>> GetPhoneBooksAsync()
        {
           return await _repository.Entities.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task DeletePhoneBookAsync(int id)
        {
            var entity = await GetPhoneBookAsync(id);

            if (entity == null)
            {
                throw new NotFoundException("Phone book not found");
            }

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<PhoneBook> GetPhoneBookAsync(int id)
        {
            Guard.ThrowIfDefaultValue(id, "Phone book");

            var phoneBook = await
                _repository.FindByIdAsync(id);

            return phoneBook;
        }

        private async Task ValidateAlreadyExists(string name, int? phoneBookId = null)
        {
            var exists = await
                _repository.Entities.AnyAsync(p => p.Name.ToLower() == name.ToLower() 
                                                   && p.Id != phoneBookId);

            if (exists)
            {
                throw new ValidateException("Phone book already exits");
            }
        }
    }
}
