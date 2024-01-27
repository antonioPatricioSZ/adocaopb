using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryUser;

public interface IUserReadOnlyRepository {

    Task<bool> UserEmailExists(string email);
    Task<User> GetUserById(string userId);
    Task GetUserByEmail(string email);
    Task<IList<User>> GetAllUsers();

}
