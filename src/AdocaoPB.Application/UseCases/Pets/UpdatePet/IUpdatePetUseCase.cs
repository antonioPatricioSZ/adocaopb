using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.Pets.UpdatePet;

public interface IUpdatePetUseCase {

    Task Execute(long idPet, RequestUpdatePetJson request);

}
