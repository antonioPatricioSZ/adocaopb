
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Application.UseCases.Authorizations.GetAllRolesUser;

public class GetAllRolesUserUseCase : IGetAllRolesUserUseCase {

    private readonly UserManager<Domain.Entities.User> _userManager;

    public GetAllRolesUserUseCase(UserManager<Domain.Entities.User> userManager) {
        _userManager = userManager;
    }

    public async Task<IList<string>> Execute(string email) {

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) {
            throw new NotFoundException(ResourceErrorMessages.EMAIL_NAO_REGISTRADO);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        return userRoles;

    }

}
