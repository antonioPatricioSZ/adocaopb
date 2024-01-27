namespace AdocaoPB.Communication.Responses;

public class ResponseAddPet {

    public long Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public string AdopterId { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public string Color { get; set; }
    public string Breed { get; set; }
    public string Observations { get; set; }
    public bool IsAvailable { get; set; }

}
