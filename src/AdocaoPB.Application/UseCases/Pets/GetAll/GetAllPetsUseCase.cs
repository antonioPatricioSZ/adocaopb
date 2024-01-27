using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AutoMapper;

namespace AdocaoPB.Application.UseCases.Pets.GetAll;

public class GetAllPetsUseCase : IGetAllPetsUseCase {

    private readonly IMapper _mapper;
    private readonly IPetReadOnlyRepository _repository;

    public GetAllPetsUseCase(
        IMapper mapper,
        IPetReadOnlyRepository repository
    ){
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ResponseGetAllPets>> Execute(
        int pageNumber,
        int pageQuantity
    ){

        var pets = await _repository.GetAll(pageNumber, pageQuantity);
        var result = _mapper.Map<List<ResponseGetAllPets>>(pets);
        
        return result;
    }

}
