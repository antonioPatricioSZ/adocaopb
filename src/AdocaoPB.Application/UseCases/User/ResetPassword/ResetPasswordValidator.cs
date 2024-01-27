﻿using AdocaoPB.Communication.Requests;
using AdocaoPB.Exceptions;
using FluentValidation;

namespace AdocaoPB.Application.UseCases.User.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<RequestResetPasswordJson> {

    public ResetPasswordValidator() {

        RuleFor(request => request.NewPassword)
            .SetValidator(new PasswordValidator());

        RuleFor(request => request.ConfirmNewPassword)
            .SetValidator(new ConfirmPasswordValidator());

        RuleFor(request => request.ConfirmNewPassword)
            .Equal(request => request.NewPassword)
            .WithMessage(ResourceErrorMessages.PASSWORD_AND_CONFIRMATION_ARE_DIFFERENT);

    }

}
