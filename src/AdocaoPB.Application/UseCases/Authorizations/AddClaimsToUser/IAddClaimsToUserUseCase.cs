using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.Authorizations.AddClaimsToUser;

public interface IAddClaimsToUserUseCase {

    Task Execute(RequestAddClaimsToUser request);

}
