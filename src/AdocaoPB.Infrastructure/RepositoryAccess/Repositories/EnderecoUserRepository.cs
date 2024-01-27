using Microsoft.EntityFrameworkCore;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories.RepositoryEnderecoUser;


namespace AdocaoPB.Infrastructure.RepositoryAccess.Repositories;

public class EnderecoUserRepository : IEnderecoUserReadOnlyRepository, 
    IEnderecoUserWriteOnlyRepository, IEnderecoUserUpdateOnlyRepository
{

    private readonly AdocaoPBContext _context;

    public EnderecoUserRepository(AdocaoPBContext context) {
        _context = context;
    }

    public async Task Add(EnderecoUsers enderecoUser) {
        await _context.EnderecoUsers.AddAsync(enderecoUser);
    }

    public async Task<EnderecoUsers> GetEnderecoUser(string idUser) {
        return await _context.EnderecoUsers.AsNoTracking()
            .FirstOrDefaultAsync(endereco => endereco.UserId.Equals(idUser));
    }

    public void Update(EnderecoUsers enderecoUsers) {
        _context.EnderecoUsers.Update(enderecoUsers);
    }
}
