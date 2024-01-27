﻿namespace AdocaoPB.Communication.Requests;

public class RequestAddEndereco {

    public long Id { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Logradouro { get; set; }
    public string Estado { get; set; }
    public string? Complemento { get; set; }

}
