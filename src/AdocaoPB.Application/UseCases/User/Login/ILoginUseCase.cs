using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.User.Login;

public interface ILoginUseCase {

    Task<JsonRegisteredUserResponse> Execute(RequestLoginJson request);

}
