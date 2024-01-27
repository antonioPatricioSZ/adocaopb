namespace AdocaoPB.Communication.Responses;

public class JsonErrorResponse {

    public List<string> Messages { get; set; }

    public JsonErrorResponse(string message) {
        Messages = new List<string> {
            message
        };
    }

    public JsonErrorResponse(List<string> messages) {
        Messages = messages;
    }

}
