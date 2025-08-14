using ContactosApp.Server.Models;
using System.Collections.Concurrent;

namespace ContactosApp.Server.Repositories
{
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly ConcurrentDictionary<int, Contact> _store = new();
        private int _nextId = 1;
        public Task<Contact?> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var contact);
            return Task.FromResult(contact);
        }

        public Task<IEnumerable<Contact>> GetAllAsync(string? search = null)
        {
            IEnumerable<Contact> result = _store.Values
           .OrderBy(c => c.Id);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLowerInvariant();
                result = result.Where(c => c.Name.ToLowerInvariant().Contains(s));
            }

            return Task.FromResult(result);
        }
        public Task<Contact> AddAsync(Contact contact)
        {
            var id = Interlocked.Increment(ref _nextId);
            contact.Id = id;
            contact.CreatedAt = DateTime.UtcNow;
            _store[id] = contact;
            return Task.FromResult(contact);
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            // Convertimos a minúsculas para que la búsqueda sea case-insensitive
            var exists = _store.Values.Any(c =>
                !string.IsNullOrWhiteSpace(c.Email) &&
                c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
            );
            return Task.FromResult(exists);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<Contact> UpdateAsync(Contact contact)
        {
            _store[contact.Id] = contact;
            return Task.FromResult(contact);
        }

    }
}
