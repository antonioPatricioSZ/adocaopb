using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using AutoMapper;

namespace AdocaoPB.Application.UseCases.User.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase {

    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetAllUsersUseCase(
        IMapper mapper,
        IUserReadOnlyRepository userReadOnlyRepository
    ){
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<IList<ResponseGetAllUsersJson>> Execute() {
        
        var users = await _userReadOnlyRepository.GetAllUsers();

        return _mapper.Map<List<ResponseGetAllUsersJson>>(users);

    }
}
