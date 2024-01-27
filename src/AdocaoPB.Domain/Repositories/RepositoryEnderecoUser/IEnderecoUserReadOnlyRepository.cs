using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryEnderecoUser;

public interface IEnderecoUserReadOnlyRepository {

    Task<EnderecoUsers> GetEnderecoUser(string idUser); 

}
