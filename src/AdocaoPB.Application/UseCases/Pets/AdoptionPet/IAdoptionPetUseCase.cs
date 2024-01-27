namespace AdocaoPB.Application.UseCases.Pets.AdoptionPet;

public interface IAdoptionPetUseCase {

    Task Execute(long idPet);

}
