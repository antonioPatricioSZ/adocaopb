using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdocaoPB.Infrastructure.RepositoryAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository,
    IUserReadOnlyRepository, IUserUpdateOnlyRepository {

    private readonly UserManager<User> _userManager;
    private readonly AdocaoPBContext _context;

    public UserRepository(UserManager<User> userManager, AdocaoPBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Add(User user, string password) {
        await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> UserEmailExists(string email) {
        var result = await _userManager.FindByEmailAsync(email);

        return result is not null;
    }

    public async Task<User> GetUserById(string userId) {
        return await _context.Users
            .Include(user => user.EnderecoUser)
            .FirstOrDefaultAsync(user => user.Id.Equals(userId));
    }

    public async Task GetUserByEmail(string email) {
        var user = await _context.Users.ToListAsync();
        Console.WriteLine(user);
            
        var teste = "";
    }

    public async Task ResetPassword(User user) {
        await _userManager.UpdateAsync(user);
    }

    public async Task<IList<User>> GetAllUsers() {
        return await _context.Users.AsNoTracking().ToListAsync();
    }
}
