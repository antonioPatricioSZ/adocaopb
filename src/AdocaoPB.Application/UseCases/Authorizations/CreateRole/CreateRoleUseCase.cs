
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Application.UseCases.Authorizations.CreateRole;

public class CreateRoleUseCase : ICreateRoleUseCase {


    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateRoleUseCase(
        UserManager<Domain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager
    ){
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Execute(string nameRole) {
        
        var roleExists = await _roleManager.RoleExistsAsync(nameRole);

        if (!roleExists) {

            var roleResult = await _roleManager.CreateAsync(
                new IdentityRole(nameRole)    
            );

            if(!roleResult.Succeeded) {
                throw new ValidationErrorsException(
                    new List<string> {
                        ResourceErrorMessages.ERRO_ADD_ROLE
                    }
                );
            }

        } else {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ROLE_JA_EXISTE
                }
            );
        }

        

    }

}
