using System.Runtime.Serialization;

namespace AdocaoPB.Exceptions.ExceptionsBase;

[Serializable]
public class NotFoundException : AdocaoPBException {

    public NotFoundException(string message) : base(message) {}

    protected NotFoundException(SerializationInfo info, StreamingContext context)
       : base(info, context) { }

}
