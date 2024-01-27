namespace AdocaoPB.Application.UseCases.User.SendEmail;

public interface ISendEmailUseCase {

    Task<Domain.Entities.User?>  Execute(string email);

}
