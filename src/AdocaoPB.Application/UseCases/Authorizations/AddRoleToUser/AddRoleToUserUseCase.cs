
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using Microsoft.AspNetCore.Identity;
using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.Authorizations.AddRoleToUser;

public class AddRoleToUserUseCase : IAddRoleToUserUseCase {

    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AddRoleToUserUseCase(
        UserManager<Domain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager
    ){
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Execute(RequestAddUserToRole request) {

        var user = await Validate(request);

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);

        if(!result.Succeeded) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ERRO_ADD_USER_TO_ROLE
                }
            );
        }

    }


    private async Task<Domain.Entities.User?> Validate(RequestAddUserToRole request) {

        var userEmailExists = await _userManager.FindByEmailAsync(request.EmailUser);

        if(userEmailExists is null) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.EMAIL_NAO_REGISTRADO
                }
            );
        }


        var roleExist = await _roleManager.RoleExistsAsync(request.RoleName);
        
        if(!roleExist) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ROLE_NAO_EXISTE
                }
            );
        }

        return userEmailExists;

    }

}
