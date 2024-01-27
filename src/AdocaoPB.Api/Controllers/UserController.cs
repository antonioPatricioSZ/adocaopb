using AdocaoPB.Api.Filters;
using AdocaoPB.Application.UseCases.User.GetAllUsers;
using AdocaoPB.Application.UseCases.User.GetById;
using AdocaoPB.Application.UseCases.User.SendEmail;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdocaoPB.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {

    private readonly IUserReadOnlyRepository _repository;

    public UserController(IUserReadOnlyRepository repository) {
        _repository = repository;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserById(
        [FromServices] IGetUserByIdUseCase useCase
    ){

        var user = await useCase.Execute();

        return Ok(user);

    }


    [HttpPost]
    [Route("SendEmail")]
    public async Task<IActionResult> SendEMail(
        [FromQuery] string email,
        [FromServices] ISendEmailUseCase useCase
    ){
        var user = await useCase.Execute(email);

        return Ok(user);
    }


    [HttpGet]
    [Route("GetAllUsers")]
    [ProducesResponseType(typeof(List<ResponseGetAllUsersJson>), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ClaimsAuthorize("AuthRoles", "Ler")]
    public async Task<IActionResult> GetAllUsers(
        [FromServices] IGetAllUsersUseCase useCase
    ){

        var users = await useCase.Execute();

        return Ok(users);

    }

}
