using ContactosApp.Server.Models;

namespace ContactosApp.Server.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync(string? search = null);
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Contact> UpdateAsync(Contact contact);

    }
}
