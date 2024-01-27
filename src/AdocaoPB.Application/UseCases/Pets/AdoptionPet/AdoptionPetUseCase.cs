using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Pets.AdoptionPet;

public class AdoptionPetUseCase : IAdoptionPetUseCase {


    private readonly IPetUpdateOnlyRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public AdoptionPetUseCase(
        IPetUpdateOnlyRepository repository,
        IHttpContextAccessor contextAccessor,
        IUnitOfWork unitOfWork
    )
    {
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

        if (pet.OwnerId == userId) {
            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.ERRO_ADOCAO_PROPRIO_PET
                }
            );
        }

        if (pet.AdopterId is not null) {
            if(pet.AdopterId == userId) {
                throw new ValidationErrorsException(
                    new List<string> {
                        ResourceErrorMessages.PEDIDO_ADOCAO_JA_FEITO
                    }
                );
            }

            throw new ValidationErrorsException(
                new List<string> {
                    ResourceErrorMessages.PET_JA_POSSUI_PROCESSO_ADOCAO
                }
            );

        }

        pet.AdopterId = userId;

        _repository.AdoptionPet(pet);

        await _unitOfWork.Commit();

    }

}
