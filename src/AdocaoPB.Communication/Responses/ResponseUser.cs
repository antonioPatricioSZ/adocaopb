namespace AdocaoPB.Communication.Responses;

public class ResponseUser {

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public ResponseEnderecoUser? ResponseEnderecoUser { get; set; }

}
