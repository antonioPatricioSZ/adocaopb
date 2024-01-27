using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryUser;

public interface IUserUpdateOnlyRepository {

    Task ResetPassword(User user);

}
