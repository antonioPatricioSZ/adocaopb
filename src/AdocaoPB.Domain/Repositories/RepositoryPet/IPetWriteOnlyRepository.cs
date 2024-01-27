namespace AdocaoPB.Domain.Repositories.RepositoryPet;

public interface IPetWriteOnlyRepository {

    Task Add(Entities.Pet pet);

    Task Delete(long idPet);

}
