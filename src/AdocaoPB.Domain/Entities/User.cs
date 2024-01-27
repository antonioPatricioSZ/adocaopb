using Microsoft.AspNetCore.Identity;

namespace AdocaoPB.Domain.Entities;

public class User : IdentityUser {

    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public EnderecoUsers? EnderecoUser { get; set; }

}
