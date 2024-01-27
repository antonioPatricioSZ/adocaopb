namespace AdocaoPB.Application.UseCases.User.GetById;

public interface IGetUserByIdUseCase {

    Task<ResponseGetUserById> Execute();

}
