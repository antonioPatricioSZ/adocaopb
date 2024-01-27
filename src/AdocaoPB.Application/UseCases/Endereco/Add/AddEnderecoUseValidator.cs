using AdocaoPB.Communication.Requests;
using AdocaoPB.Exceptions;
using FluentValidation;

namespace AdocaoPB.Application.UseCases.Endereco.Add;

public class AddEnderecoUseValidator : AbstractValidator<RequestAddEndereco> {

    public AddEnderecoUseValidator() {

        RuleFor(request => request.Bairro).NotEmpty()
            .WithMessage(ResourceErrorMessages.BAIRRO_ENDERECO_EM_BRANCO);

        RuleFor(request => request.Numero).NotEmpty()
            .WithMessage(ResourceErrorMessages.NUMERO_ENDERECO_EM_BRANCO);

        RuleFor(request => request.Cidade).NotEmpty()
            .WithMessage(ResourceErrorMessages.CIDADE_ENDERECO_EM_BRANCO);

        RuleFor(request => request.Logradouro).NotEmpty()
            .WithMessage(ResourceErrorMessages.LOGRADOURO_ENDERECO_EM_BRANCO);

        RuleFor(request => request.Estado).NotEmpty()
            .WithMessage(ResourceErrorMessages.ESTADO_ENDERECO_EM_BRANCO);

    }

}
