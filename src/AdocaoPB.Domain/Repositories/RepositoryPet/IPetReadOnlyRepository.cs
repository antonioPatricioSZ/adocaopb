using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryPet;

public interface IPetReadOnlyRepository {

    Task<Pet> GetById(long idPet);
    Task<IList<Pet>> GetAll(int pageNumber, int pageQuantity);
    Task<IList<Pet>> GetAllPetsForAdopter(string idAdopter);
    Task<IList<Pet>> GetAllPetsForUser(string idOwner);

}
