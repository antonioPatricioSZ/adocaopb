using AdocaoPB.Communication.Requests;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Pets.UpdatePet;

public class UpdatePetUseCase : IUpdatePetUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetReadOnlyRepository _readOnlyRepository;
    private readonly IPetUpdateOnlyRepository _updateOnlyRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public UpdatePetUseCase(
        IUnitOfWork unitOfWork,
        IPetReadOnlyRepository readOnlyRepository,
        IPetUpdateOnlyRepository updateOnlyRepository,
        IHttpContextAccessor contextAccessor,
        IMapper mapper
    ){
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
        _updateOnlyRepository = updateOnlyRepository;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }


    public async Task Execute(long idPet, RequestUpdatePetJson request) {

        var userId = _contextAccessor.HttpContext.User
            .FindFirst("id").Value;

        var pet = await _readOnlyRepository.GetById(idPet);

        Validate(userId, pet, request);

        _mapper.Map(request, pet);

        _updateOnlyRepository.Update(pet);

        await _unitOfWork.Commit();

    }


    private static void Validate(
        string userId,
        Domain.Entities.Pet pet,
        RequestUpdatePetJson request
    ){

        if(pet is null || pet.OwnerId != userId) {

            var errorMessages = new List<string> { 
                ResourceErrorMessages.PET_NAO_ENCONTRADO
            };

            throw new ValidationErrorsException(errorMessages);
        }

        var validator = new UpdatePetValidator();
        var result = validator.Validate(request);

        if(!result.IsValid) {
            var errorMessages = result.Errors.
                Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

    }

}
