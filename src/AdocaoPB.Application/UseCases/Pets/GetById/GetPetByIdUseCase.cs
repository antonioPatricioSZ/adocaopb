using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Application.UseCases.Pets.GetById;

public class GetPetByIdUseCase : IGetPetByIdUseCase {

    private readonly IMapper _mapper;
    private readonly IPetReadOnlyRepository _repository;
    //private readonly IHttpContextAccessor _contextAccessor;

    public GetPetByIdUseCase(
        IMapper mapper,
        IPetReadOnlyRepository repository
        //IHttpContextAccessor contextAccessor
    ){
        _mapper = mapper;
        _repository = repository;
        //_contextAccessor = contextAccessor;
    }

    public async Task<ResponseGetPetById> Execute(long idPet) {

        //var userId = _contextAccessor.HttpContext.User
        //    .FindFirst("id").Value;

        var pet = await _repository.GetById(idPet);

        if(pet is null) {
            throw new NotFoundException(ResourceErrorMessages.PET_NAO_ENCONTRADO);
        }

        return _mapper.Map<ResponseGetPetById>(pet);

    }

}
