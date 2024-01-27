using AdocaoPB.Domain.Repositories.RepositoryPet;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Pets.GetAllAdopter;

public class GetAllPetsAdopterUseCase : IGetAllPetsAdopterUseCase {

    private readonly IMapper _mapper;
    private readonly IPetReadOnlyRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllPetsAdopterUseCase(
        IMapper mapper,
        IPetReadOnlyRepository repository,
        IHttpContextAccessor contextAccessor
    ){
        _mapper = mapper;
        _repository = repository;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<ResponseToPetsForAdopter>> Execute() {

        var userId = _contextAccessor.HttpContext.User.FindFirst("id").Value;
        
        var myAdoptions = await _repository.GetAllPetsForAdopter(userId);

        return _mapper.Map<List<ResponseToPetsForAdopter>>(myAdoptions);

    }

}
