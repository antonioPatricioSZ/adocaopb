using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryEnderecoUser;

public interface IEnderecoUserWriteOnlyRepository {

    Task Add(EnderecoUsers enderecoUser);

}
