using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Http;


namespace AdocaoPB.Application.UseCases.Pets.Add;

public class AddPetUseCase : IAddPetUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetWriteOnlyRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public AddPetUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPetWriteOnlyRepository repository,
        IHttpContextAccessor httpContext
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<ResponseAddPet> Execute(RequestAddPet request) {

        Validate(request);

        var pet = _mapper.Map<Domain.Entities.Pet>(request);

        var dataNormilized = TimeZoneInfo
            .ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(
                    "E. South America Standard Time"
                )
            );

        var userIdClaim = _httpContext.HttpContext?.User.FindFirst("id");
        var userId = userIdClaim.Value;
        pet.OwnerId = userId;
        pet.AdopterId = "592fcbbe-3074-4b9d-a6f6-f19d6f0d6c7f";
        pet.CreationDate = dataNormilized;

        await _repository.Add(pet);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseAddPet>(pet);

    }

    private void Validate(RequestAddPet request) {
        
        var validator = new AddPetValidator();
        var result = validator.Validate(request);

        if(!result.IsValid) {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();

            throw new ValidationErrorsException(errorMessages);
        }

    }

}
