using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Domain.Repositories.RepositoryPet;

public interface IPetUpdateOnlyRepository {

    Task<Pet> RecuperarPetPorIdUpdate(long idPet);

    void AdoptionPet(Pet pet);

    void ConcludeAdoptionPet(Pet pet);

    void Update(Pet pet);

}
