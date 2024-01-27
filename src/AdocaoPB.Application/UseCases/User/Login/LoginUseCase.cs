using AdocaoPB.Application.Services.Token;
using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Application.UseCases.User.Login;

public class LoginUseCase : ILoginUseCase {

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenController _tokenController;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenRepository;

    public LoginUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        TokenController tokenController,
        UserManager<Domain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager,
        IRefreshTokenWriteOnlyRepository refreshTokenRepository
    ){
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenController = tokenController;
        _userManager = userManager;
        _roleManager = roleManager;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<JsonRegisteredUserResponse> Execute(RequestLoginJson request) {

        var userExist = await _userManager.FindByEmailAsync(request.Email);

        if (userExist is null) {
            throw new InvalidLoginException();
        }

        var isCorrect = await _userManager
                .CheckPasswordAsync(userExist, request.Password);

        if (!isCorrect) {
            throw new InvalidLoginException();
        }

        var result = await _tokenController.GenerateJwtToken(userExist);
        
        return new JsonRegisteredUserResponse
        {
            Token = result.Token,
            RefreshToken = result.RefreshToken
        };

    }

}
