using System.Security.Claims;

namespace AdocaoPB.Application.UseCases.Authorizations.GetAllClaimsUser;

public interface IGetAllClaimsUserUseCase {

    Task<IList<Claim>> Execute(string email);

}
