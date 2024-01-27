﻿namespace AdocaoPB.Domain.Entities;

public class EnderecoUsers : BaseEntity {

    public long Id { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Logradouro { get; set; }
    public string Estado { get; set; }
    public string? Complemento { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

}