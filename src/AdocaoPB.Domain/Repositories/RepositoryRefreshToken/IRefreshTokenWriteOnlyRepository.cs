using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryRefreshToken;

public interface IRefreshTokenWriteOnlyRepository {

    Task Add(RefreshToken refreshToken);
    Task Delete(long idRefreshToken);
    Task<RefreshToken> RefreshTokenUserExists(string userId);
    Task<RefreshToken> RefreshTokenGetById(string refreshToken);
    void Update(RefreshToken refreshToken);
}
