namespace AdocaoPB.Application.UseCases.Authorizations.GetAllRolesUser;

public interface IGetAllRolesUserUseCase {

    Task<IList<string>> Execute(string email);

}
