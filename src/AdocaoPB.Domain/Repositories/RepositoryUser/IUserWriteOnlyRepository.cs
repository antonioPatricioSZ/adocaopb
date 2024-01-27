using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryUser;

public interface IUserWriteOnlyRepository {

    Task Add(User user, string password);

}
