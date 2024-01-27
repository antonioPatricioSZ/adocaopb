using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Application.UseCases.Pets.GetById;

public interface IGetPetByIdUseCase {

    Task<ResponseGetPetById> Execute(long idPet);

}
