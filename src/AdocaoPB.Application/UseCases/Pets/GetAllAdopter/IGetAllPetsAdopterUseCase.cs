namespace AdocaoPB.Application.UseCases.Pets.GetAllAdopter;

public interface IGetAllPetsAdopterUseCase {

    Task<List<ResponseToPetsForAdopter>> Execute();

}
