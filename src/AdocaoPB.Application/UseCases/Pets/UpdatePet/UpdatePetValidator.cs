using AdocaoPB.Communication.Requests;
using AdocaoPB.Exceptions;
using FluentValidation;

namespace AdocaoPB.Application.UseCases.Pets.UpdatePet;

public class UpdatePetValidator : AbstractValidator<RequestUpdatePetJson> {

    public UpdatePetValidator() {

        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_PET_EM_BRANCO);

        RuleFor(request => request.Weight).NotEmpty()
            .WithMessage(ResourceErrorMessages.PESO_PET_EM_BRANCO);

        RuleFor(request => request.Age).NotEmpty()
            .WithMessage(ResourceErrorMessages.IDADE_PET_EM_BRANCO);

        RuleFor(request => request.Color).NotEmpty()
            .WithMessage(ResourceErrorMessages.COR_PET_EM_BRANCO);

        RuleFor(request => request.Breed).NotEmpty()
            .WithMessage(ResourceErrorMessages.RACA_PET_EM_BRANCO);

    }

}
