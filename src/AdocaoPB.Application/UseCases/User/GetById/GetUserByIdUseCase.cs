
using AdocaoPB.Domain.Repositories.RepositoryUser;
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.UseCases.User.GetById;

public class GetUserByIdUseCase : IGetUserByIdUseCase {

    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserByIdUseCase(
        IMapper mapper,
        IUserReadOnlyRepository repository,
        IHttpContextAccessor contextAccessor
    ){
        _mapper = mapper;
        _repository = repository;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResponseGetUserById> Execute() {

        var userId = _contextAccessor.HttpContext.User
            .FindFirst("id").Value;

        var user = await _repository.GetUserById(userId);

        if (user is null) {
            throw new NotFoundException(ResourceErrorMessages.PET_NAO_ENCONTRADO);
        }

        return _mapper.Map<ResponseGetUserById>(user);

    }

}
