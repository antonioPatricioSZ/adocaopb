using AdocaoPB.Communication.Requests;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using AdocaoPB.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Application.UseCases.User.ResetPassword;

public class ResetPasswordUseCase : IResetPasswordUseCase {

    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserUpdateOnlyRepository _updateOnlyRepository;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordUseCase(
        IHttpContextAccessor contextAccessor,
        IUserUpdateOnlyRepository updateOnlyRepository,
        UserManager<Domain.Entities.User> userManager,
        IUnitOfWork unitOfWork
    ){
        _contextAccessor = contextAccessor;
        _updateOnlyRepository = updateOnlyRepository;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }


    public async Task Execute(string userId, RequestResetPasswordJson request) {

        Validate(request);

        //var userId = _contextAccessor.HttpContext.User.
        //    FindFirst("id").Value;

        var user = await _userManager.FindByIdAsync(userId);

        var newPasswordHash = _userManager.PasswordHasher
            .HashPassword(user, request.NewPassword);

        user.PasswordHash = newPasswordHash;

        await _updateOnlyRepository.ResetPassword(user);
        await _unitOfWork.Commit();

    }


    private static void Validate(RequestResetPasswordJson request) {

        var validator = new ResetPasswordValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) {

            var errorMessages = result.Errors
                .Select(erro => erro.ErrorMessage).ToList();

            throw new ValidationErrorsException(errorMessages);

        }

    }

}
