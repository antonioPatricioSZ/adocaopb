using System.Runtime.Serialization;

namespace AdocaoPB.Exceptions.ExceptionsBase;

[Serializable]
public class ForbidenException : AdocaoPBException {

    public ForbidenException(string message) : base(message) { }


    protected ForbidenException(SerializationInfo info, StreamingContext context)
       : base(info, context) { }

}
