using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.Authorizations.AddRoleToUser;

public interface IAddRoleToUserUseCase {

    Task Execute(RequestAddUserToRole request);
     
}
