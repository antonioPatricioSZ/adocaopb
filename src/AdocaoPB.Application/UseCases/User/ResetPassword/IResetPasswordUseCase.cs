using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.User.ResetPassword;

public interface IResetPasswordUseCase {

    Task Execute(string userId, RequestResetPasswordJson request);

}
