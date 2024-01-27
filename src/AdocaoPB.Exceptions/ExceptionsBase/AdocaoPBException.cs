using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;

namespace AdocaoPB.Exceptions.ExceptionsBase;

[Serializable]
public class AdocaoPBException : SystemException {

    public AdocaoPBException(string message) : base(message) {}

    protected AdocaoPBException(SerializationInfo info, StreamingContext context)
       : base(info, context) { }

}

// A marcação[Serializable] em C# é usada para indicar que
// uma classe pode ser serializada, ou seja, pode ser
// convertida em uma sequência de bytes para ser armazenada
// em um arquivo, transmitida pela rede ou armazenada em
// memória, e posteriormente desserializada de volta para
// um objeto. Essencialmente, a serialização é o processo
// de converter um objeto em um formato que pode ser facilmente
// armazenado ou transmitido e depois reconstruído.

// Quando uma classe é marcada como[Serializable], isso
// significa que ela pode ser convertida em um formato
// serializado, o que pode ser útil em várias situações,
// como persistência de dados, comunicação entre diferentes
// sistemas ou componentes, entre outros.
// A classe NotFoundException herda da classe AdocaoPBException
// e é marcada como[Serializable]. A declaração do construtor
// protected NotFoundException(SerializationInfo info,
// StreamingContext context) : base(info, context) é parte
// da implementação de serialização dessa classe.

// Quando um objeto serializado é desserializado, ou seja,
// convertido de volta para um objeto, pode ser necessário
// recriar o estado interno desse objeto.O construtor protegido
// NotFoundException(SerializationInfo info, StreamingContext
// context) é usado para recriar um objeto NotFoundException
// a partir de um objeto serializado (SerializationInfo info)
// e o contexto de streaming (StreamingContext context).
// Esse construtor permite que a classe lide com a
// desserialização de objetos dessa classe específica.

// Em resumo, a marcação [Serializable] permite que a classe
// seja serializada e o construtor protegido
// NotFoundException(SerializationInfo info,
// StreamingContext context) é usado para suportar a
// desserialização da classe NotFoundException a partir de
// um formato serializado.
