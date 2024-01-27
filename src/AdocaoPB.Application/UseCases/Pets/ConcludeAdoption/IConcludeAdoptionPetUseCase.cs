namespace AdocaoPB.Application.UseCases.Pets.ConcludeAdoption;

public interface IConcludeAdoptionPetUseCase {

    Task Execute(long idPet);

}
