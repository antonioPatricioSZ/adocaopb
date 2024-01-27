namespace AdocaoPB.Domain.Repositories.RepositoryEmail.SendEmail;

public interface ISendEmail {

    void Send(string emailsTo, string subject, string message);

}
