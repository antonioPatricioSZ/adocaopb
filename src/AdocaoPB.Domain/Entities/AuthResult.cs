using AdocaoPB.Communication.Requests;

namespace AdocaoPB.Domain.Entities;

public class AuthResult {

    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
    public RequestAddRefreshToken RefreshTokenRequest { get; set; }

}
