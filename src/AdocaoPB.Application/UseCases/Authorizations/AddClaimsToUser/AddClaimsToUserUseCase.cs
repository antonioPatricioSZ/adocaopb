using AdocaoPB.Communication.Requests;
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdocaoPB.Application.UseCases.Authorizations.AddClaimsToUser;

public class AddClaimsToUserUseCase : IAddClaimsToUserUseCase {

    private readonly UserManager<Domain.Entities.User> _userManager;

    public async Task Execute(RequestAddClaimsToUser request) {
        var user = await _userManager.FindByEmailAsync(request.EmailUser);

        if (user is null) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.EMAIL_NAO_REGISTRADO
                }
            );
        }

        var userClaim = new Claim(request.ClaimName, request.ClaimValue);

        var result = await _userManager.AddClaimAsync(user, userClaim);

        if (!result.Succeeded) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ERRO_ADD_CLAIM_TO_USER
                }    
            );
        }

    }

}
