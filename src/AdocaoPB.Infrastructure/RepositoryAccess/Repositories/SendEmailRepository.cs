using AdocaoPB.Domain.Repositories.RepositoryEmail.SendEmail;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace AdocaoPB.Infrastructure.RepositoryAccess.Repositories;

public class SendEmailRepository : ISendEmail {

    private readonly string ApiKey = "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";

    public void Send(string emailsTo, string subject, string message)
    {

        Configuration.Default.ApiKey["api-key"] = ApiKey;

        var apiInstance = new TransactionalEmailsApi();

        string SenderName = "Jucylane Gomes";
        string SenderEmail = "jug73881@gmail.com";
        SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

        SendSmtpEmailTo emailReceiver1 = new SendSmtpEmailTo(emailsTo);
        List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
        To.Add(emailReceiver1);

        string HtmlContent = message;
        string TextContent = null;

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(
                emailSender,
                To,
                null,
                null,
                HtmlContent,
                TextContent,
                subject
            );

            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Console.WriteLine("Response: \n " + result.ToJson());
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro: " + e.Message);
        }

    }

}
