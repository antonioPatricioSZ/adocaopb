using AdocaoPB.Communication.Requests;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryEnderecoUser;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.Endereco.Add;

public class AddEnderecoUseCase : IAddEnderecoUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnderecoUserWriteOnlyRepository _repository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IEnderecoUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IEnderecoUserUpdateOnlyRepository _userUpdateOnlyRepository;

    public AddEnderecoUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IEnderecoUserWriteOnlyRepository repository,
        IHttpContextAccessor httpContext,
        IEnderecoUserReadOnlyRepository userReadOnlyRepository,
        IEnderecoUserUpdateOnlyRepository userUpdateOnlyRepository
    ){
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _httpContext = httpContext;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
    }

    public async Task Execute(RequestAddEndereco request) {

        Validate(request);

        //Validate(request);
        var userId = _httpContext.HttpContext.User
            .FindFirst("id").Value;

        var enderecoUser = await _userReadOnlyRepository.GetEnderecoUser(userId);

        if(enderecoUser is not null) {
            enderecoUser.Estado = request.Estado;
            enderecoUser.Numero = request.Numero;
            enderecoUser.Cidade = request.Cidade;
            enderecoUser.Bairro = request.Bairro;
            enderecoUser.Logradouro = request.Logradouro;
            enderecoUser.Complemento = request.Complemento;

            _userUpdateOnlyRepository.Update(enderecoUser);
            await _unitOfWork.Commit();

        } else {
            var endereco = _mapper.Map<Domain.Entities.EnderecoUsers>(request);
            endereco.UserId = userId;

            await _repository.Add(endereco);
            await _unitOfWork.Commit();
        }



    }


    public async void Validate(RequestAddEndereco request) {

        var validator = new AddEnderecoUseValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();

            throw new ValidationErrorsException(errorMessages);
        }

    }

}
