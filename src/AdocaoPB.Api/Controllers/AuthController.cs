using AdocaoPB.Api.Filters;
using AdocaoPB.Application.Services.Token;
using AdocaoPB.Application.UseCases.Authorizations.AddClaimsToUser;
using AdocaoPB.Application.UseCases.Authorizations.AddRoleToUser;
using AdocaoPB.Application.UseCases.Authorizations.CreateRole;
using AdocaoPB.Application.UseCases.Authorizations.GetAllClaimsUser;
using AdocaoPB.Application.UseCases.Authorizations.GetAllRolesUser;
using AdocaoPB.Application.UseCases.User.Login;
using AdocaoPB.Application.UseCases.User.Register;
using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdocaoPB.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {

    private readonly UserManager<User> _userManager;
    private readonly TokenController _tokenController;

    public AuthController(
        UserManager<User> userManager,
        TokenController tokenController
    ){
        _userManager = userManager;
        _tokenController = tokenController;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase
    )
    {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);

    }


    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromBody] RequestLoginJson request,
        [FromServices] ILoginUseCase useCase
    )
    {

        var result = await useCase.Execute(request);

        return Ok(result);

    }


    [HttpGet]
    [Route("RefreshToken")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RefreshToken() {

        var result = await _tokenController.RefreshToken();
        return Ok(result);

    }


    [HttpPost]
    [Route("CreateRole")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ClaimsAuthorize("AuthRoles", "Inserir")]
    public async Task CreateRole(
       [FromQuery] string nameRole,
       [FromServices] ICreateRoleUseCase useCase
    )
    {

        await useCase.Execute(nameRole);

    }


    [HttpPost]
    [Route("AddUserToRole")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ClaimsAuthorize("Role", "Inserir")]
    public async Task AddUserToRole(
       [FromBody] RequestAddUserToRole request,
       [FromServices] IAddRoleToUserUseCase useCase
    )
    {

        await useCase.Execute(request);

    }


    [HttpPost]
    [Route("AddClaimsToUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ClaimsAuthorize("Claim", "Inserir")]
    public async Task AddClaimsToUser(
        [FromBody] RequestAddClaimsToUser request,
        [FromServices] IAddClaimsToUserUseCase useCase
    )
    {

        await useCase.Execute(request);

    }


    [HttpGet]
    [Route("GetUserClaims")]
    public async Task<IActionResult> GetUserClaims(
        [FromQuery] string email,
        [FromServices] IGetAllClaimsUserUseCase useCase
    ){

        var userClaims = await useCase.Execute(email);

        return Ok(userClaims);

    }


    [HttpGet]
    [Route("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles(
        [FromQuery] string email,
        [FromServices] IGetAllRolesUserUseCase useCase
    ){

        var userRoles = await useCase.Execute(email);

        return Ok(userRoles);

    }





}
