using AdocaoPB.Application.UseCases.Endereco.Add;
using AdocaoPB.Communication.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdocaoPB.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoUserController : ControllerBase {

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<IActionResult> AddEndereco(
        [FromBody] RequestAddEndereco request,
        [FromServices] IAddEnderecoUseCase useCase
    ){

        await useCase.Execute(request);

        return NoContent();
    }

}
