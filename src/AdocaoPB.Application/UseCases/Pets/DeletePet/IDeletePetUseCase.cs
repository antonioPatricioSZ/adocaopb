namespace AdocaoPB.Application.UseCases.Pets.DeletePet;

public interface IDeletePetUseCase {

    Task Execute(long idPet);

}
