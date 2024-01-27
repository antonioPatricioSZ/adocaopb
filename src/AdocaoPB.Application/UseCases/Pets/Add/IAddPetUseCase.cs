using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.Pets.Add;

public interface IAddPetUseCase {

    Task<ResponseAddPet> Execute(RequestAddPet request);

}
