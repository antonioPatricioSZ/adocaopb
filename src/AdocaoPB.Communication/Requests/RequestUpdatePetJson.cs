namespace AdocaoPB.Communication.Requests;

public class RequestUpdatePetJson {

    public string Name { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public string Color { get; set; }
    public string Breed { get; set; }
    public string? Observations { get; set; }

}
