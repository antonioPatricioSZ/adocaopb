
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Pets.DeletePet;

public class DeletePetUseCase : IDeletePetUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetReadOnlyRepository _readOnlyRepository;
    private readonly IPetWriteOnlyRepository _writeOnlyRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public DeletePetUseCase(
        IUnitOfWork unitOfWork,
        IPetReadOnlyRepository readOnlyRepository,
        IPetWriteOnlyRepository writeOnlyRepository,
        IHttpContextAccessor contextAccessor
    ){
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _contextAccessor = contextAccessor;
    }


    public async Task Execute(long idPet) {

        var userId = _contextAccessor.HttpContext.User.
            FindFirst("id").Value;

        var pet = await _readOnlyRepository.GetById(idPet);

        Validate(userId, pet);

        await _writeOnlyRepository.Delete(idPet);
        await _unitOfWork.Commit();

    }


    private static void Validate(string userId, Domain.Entities.Pet pet) {

        if(pet is null || pet.OwnerId != userId ) {

            var errorMessages = new List<string> { 
                ResourceErrorMessages.PET_NAO_ENCONTRADO
            };

            throw new ValidationErrorsException(errorMessages);
        }

    }

}
