using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Pets.GetAllUser;

public class GetAllPetsUserUseCase : IGetAllPetsUserUseCase {

    private readonly IPetReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public GetAllPetsUserUseCase(
        IPetReadOnlyRepository repository,
        IMapper mapper,
        IHttpContextAccessor httpContext
    ){
        _repository = repository;
        _mapper = mapper;
        _httpContext = httpContext;
    }

    public async Task<List<ResponseToPetsForUser>> Execute() {

        var userIdClaim = _httpContext.HttpContext?.User.FindFirst("id");
        var userId = userIdClaim.Value;

        var petsUser = await _repository.GetAllPetsForUser(userId);
        var result = _mapper.Map<List<ResponseToPetsForUser>>(petsUser);

        return result;
    }
}
