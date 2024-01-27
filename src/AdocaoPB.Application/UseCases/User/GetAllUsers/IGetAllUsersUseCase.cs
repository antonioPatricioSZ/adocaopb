using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.User.GetAllUsers;

public interface IGetAllUsersUseCase {

    Task<IList<ResponseGetAllUsersJson>> Execute();

}
