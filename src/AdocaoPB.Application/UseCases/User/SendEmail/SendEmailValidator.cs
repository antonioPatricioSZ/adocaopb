using AdocaoPB.Exceptions;
using FluentValidation;

namespace AdocaoPB.Application.UseCases.User.SendEmail;

public class SendEmailValidator : AbstractValidator<string> {

    public SendEmailValidator() {

        RuleFor(request => request).NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_EM_BRANCO);

        When(request => !string.IsNullOrWhiteSpace(request), () => {
            RuleFor(request => request).EmailAddress()
                .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);
        });
        
    }

}
