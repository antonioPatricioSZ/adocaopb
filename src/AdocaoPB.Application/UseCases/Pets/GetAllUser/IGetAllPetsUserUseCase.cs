using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.Pets.GetAllUser;

public interface IGetAllPetsUserUseCase {

    Task<List<ResponseToPetsForUser>> Execute();

}
