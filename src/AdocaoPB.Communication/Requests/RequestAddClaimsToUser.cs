namespace AdocaoPB.Communication.Requests;

public class RequestAddClaimsToUser {

    public string EmailUser {  get; set; }
    public string ClaimName { get; set; }
    public string ClaimValue { get; set; }

}
