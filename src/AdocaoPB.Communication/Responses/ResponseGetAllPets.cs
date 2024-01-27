namespace AdocaoPB.Communication.Responses;

public class ResponseGetAllPets {

    public long Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public string Color { get; set; }
    public string Breed { get; set; }
    public bool IsAvailable { get; set; }

}
