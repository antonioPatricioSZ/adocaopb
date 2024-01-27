using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.User.Register;

public interface IRegisterUserUseCase {

    Task<JsonRegisteredUserResponse> Execute(RequestRegisterUserJson request);

}
