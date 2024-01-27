using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using Microsoft.EntityFrameworkCore;

namespace AdocaoPB.Infrastructure.RepositoryAccess.Repositories;

public class RefreshTokenRepository : IRefreshTokenWriteOnlyRepository {

    private readonly AdocaoPBContext _context;

    public RefreshTokenRepository(AdocaoPBContext context)
    {
        _context = context;
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<RefreshToken> RefreshTokenUserExists(string userId)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(token => token.UserId.Equals(userId));
    }

    public async Task<RefreshToken> RefreshTokenGetById(string refreshToken) {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token.Equals(refreshToken)
            );
    }

    public async Task Delete(long idRefreshToken) {
        var refreshToken = await _context.RefreshTokens.
            FirstOrDefaultAsync(token => token.Id == idRefreshToken);
        
        _context.RefreshTokens.Remove(refreshToken);
    }

    public void Update(RefreshToken refreshToken) {
        _context.RefreshTokens.Update(refreshToken);
    }

}
