using System.Runtime.Serialization;

namespace AdocaoPB.Exceptions.ExceptionsBase;

[Serializable]
public class ValidationErrorsException : AdocaoPBException {

    public List<string> ErrorMessages { get; set; }

    public ValidationErrorsException(List<string> errorMessages) : base(string.Empty) {
        ErrorMessages = errorMessages;
    }

    protected ValidationErrorsException(SerializationInfo info, StreamingContext context)
       : base(info, context) { }

}
