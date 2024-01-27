using AdocaoPB.Communication.Responses;

namespace AdocaoPB.Application.UseCases.Pets.GetAll;

public interface IGetAllPetsUseCase {

    Task<List<ResponseGetAllPets>> Execute(int pageNumber, int pageQuantity);

}
