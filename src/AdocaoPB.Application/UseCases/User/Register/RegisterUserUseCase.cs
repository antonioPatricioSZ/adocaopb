using AdocaoPB.Application.Services.Token;
using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase {

    private readonly IUserReadOnlyRepository _repositoryRead;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenController _tokenController;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenRepository;


    public RegisterUserUseCase(
        IUserWriteOnlyRepository repository,
        IUserReadOnlyRepository repositoryRead,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        TokenController tokenController,
        IRefreshTokenWriteOnlyRepository refreshTokenRepository

    ){
        _repository = repository;
        _repositoryRead = repositoryRead;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenController = tokenController;
        _refreshTokenRepository = refreshTokenRepository;
    }


    public async Task<JsonRegisteredUserResponse> Execute(RequestRegisterUserJson request) {

        await Validate(request);

        var dataNormilized = TimeZoneInfo
            .ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(
                    "E. South America Standard Time"
                )
            );

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Email = request.Email;
        user.UserName = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Name = request.Name;
        user.CreationDate = dataNormilized;

        await _repository.Add(user, request.Password);

        var result = await _tokenController.GenerateJwtToken(user);

        return new JsonRegisteredUserResponse { Token = result.Token, RefreshToken = result.RefreshToken };

    }


    private async Task Validate(RequestRegisterUserJson request) {

        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var userEmailExists = await _repositoryRead
            .UserEmailExists(request.Email);

        if (userEmailExists) {
            result.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    "email",
                    ResourceErrorMessages.EMAIL_JA_REGISTRADO
                )
            );
        }

        if (!result.IsValid) {
            var errorMessages = result.Errors
                .Select(erro => erro.ErrorMessage).ToList();

            throw new ValidationErrorsException(errorMessages);
        }

    }

}
