using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Application.UseCases.Endereco.Add;

public interface IAddEnderecoUseCase {

    Task Execute(RequestAddEndereco request);

}
