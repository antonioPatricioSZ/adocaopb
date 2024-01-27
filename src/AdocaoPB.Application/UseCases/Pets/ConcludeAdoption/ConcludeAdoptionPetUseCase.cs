
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using AdocaoPB.Exceptions.ExceptionsBase;
using System.Reflection;
using AdocaoPB.Exceptions;

namespace AdocaoPB.Application.UseCases.Pets.ConcludeAdoption;

public class ConcludeAdoptionPetUseCase : IConcludeAdoptionPetUseCase {

    private readonly IPetUpdateOnlyRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public ConcludeAdoptionPetUseCase(
        IPetUpdateOnlyRepository repository,
        IHttpContextAccessor contextAccessor,
        IUnitOfWork unitOfWork
    ){
        _repository = repository;
        _contextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
    }


    public async Task Execute(long idPet) {
        

        var userId = _contextAccessor.HttpContext.User.FindFirst("id").Value;

        var pet = await _repository.RecuperarPetPorIdUpdate(idPet);

        if(pet is null) {
            throw new NotFoundException(ResourceErrorMessages.PET_NAO_ENCONTRADO);
        }

        if(userId != pet.OwnerId) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ERRO_CONCLUDE_ADOCAO
                }
            );
        }

        pet.IsAvailable = false;

        _repository.ConcludeAdoptionPet(pet);

        await _unitOfWork.Commit();

    }

}
