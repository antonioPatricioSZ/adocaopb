namespace AdocaoPB.Application.UseCases.Authorizations.CreateRole;

public interface ICreateRoleUseCase {

    Task Execute(string nameRole);

}
