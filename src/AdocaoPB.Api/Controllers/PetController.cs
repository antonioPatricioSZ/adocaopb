using AdocaoPB.Application;
using AdocaoPB.Application.UseCases.Pets.Add;
using AdocaoPB.Application.UseCases.Pets.AdoptionPet;
using AdocaoPB.Application.UseCases.Pets.ConcludeAdoption;
using AdocaoPB.Application.UseCases.Pets.DeletePet;
using AdocaoPB.Application.UseCases.Pets.GetAll;
using AdocaoPB.Application.UseCases.Pets.GetAllAdopter;
using AdocaoPB.Application.UseCases.Pets.GetAllUser;
using AdocaoPB.Application.UseCases.Pets.GetById;
using AdocaoPB.Application.UseCases.Pets.UpdatePet;
using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdocaoPB.Api.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PetController : ControllerBase {

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ResponseAddPet), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddPet(
        [FromBody] RequestAddPet request,
        [FromServices] IAddPetUseCase useCase
    ){

        var result = await useCase.Execute(request);
        
        return Created(string.Empty, result);

    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllPets), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPets(
        [FromServices] IGetAllPetsUseCase useCase,
        [FromQuery] int pageNumber, int pageQuantity
    ){

        var pets = await useCase.Execute(pageNumber, pageQuantity);

        return Ok(pets);

    }


    [HttpGet]
    [Route("{idPet}")]
    [ProducesResponseType(typeof(ResponseGetPetById), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPetById(
        [FromServices] IGetPetByIdUseCase useCase,
        [FromRoute] long idPet
    ){

        var pet = await useCase.Execute(idPet);

        return Ok(pet);

    }


    [HttpGet]
    [Route("GetAllPetsUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ResponseToPetsForUser), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPetsUser(
        [FromServices] IGetAllPetsUserUseCase useCase
    ){

        var pets = await useCase.Execute();

        return Ok(pets);

    }


    [HttpGet]
    [Route("GetAllPetsAdopter")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ResponseToPetsForAdopter), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPetsAdopter(
        [FromServices] IGetAllPetsAdopterUseCase useCase
    ){

        var pets = await useCase.Execute();

        return Ok(pets);

    }


    [HttpPut]
    [Route("adopter/{idPet}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> InitAdoptionPet (
        [FromServices] IAdoptionPetUseCase useCase,
        [FromRoute] long idPet
    ){

        await useCase.Execute(idPet);

        return NoContent();

    }


    [HttpPut]
    [Route("concludeAdoption/{idPet}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ConcludeAdoption(
        [FromServices] IConcludeAdoptionPetUseCase useCase,
        [FromRoute] long idPet
    ){

        await useCase.Execute(idPet);

        return NoContent();

    }


    [HttpDelete]
    [Route("{idPet}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePet(
        [FromServices] IDeletePetUseCase useCase,
        [FromRoute] long idPet
    ){

        await useCase.Execute(idPet);

        return NoContent();

    }


    [HttpPut]
    [Route("{idPet}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePet(
        [FromBody] RequestUpdatePetJson request,
        [FromServices] IUpdatePetUseCase useCase,
        [FromRoute] long idPet
    ){

        await useCase.Execute(idPet, request);

        return NoContent();

    }



}
