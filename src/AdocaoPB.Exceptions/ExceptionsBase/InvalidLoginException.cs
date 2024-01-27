using System.Runtime.Serialization;

namespace AdocaoPB.Exceptions.ExceptionsBase;

[Serializable]
public class InvalidLoginException : AdocaoPBException {

    public InvalidLoginException() : 
        base(ResourceErrorMessages.LOGIN_INVALIDO) {}


    protected InvalidLoginException(SerializationInfo info, StreamingContext context)
       : base(info, context) { }

}
