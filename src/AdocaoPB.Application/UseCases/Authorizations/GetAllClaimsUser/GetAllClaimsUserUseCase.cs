using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdocaoPB.Application.UseCases.Authorizations.GetAllClaimsUser;

public class GetAllClaimsUserUseCase : IGetAllClaimsUserUseCase {

    private readonly UserManager<Domain.Entities.User> _userManager;

    public GetAllClaimsUserUseCase(UserManager<Domain.Entities.User> userManager) {
        _userManager = userManager;
    }

    public async Task<IList<Claim>> Execute(string email) {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.EMAIL_NAO_REGISTRADO
                }
            );
        }       
        
        return await _userManager.GetClaimsAsync(user);

    }

}
