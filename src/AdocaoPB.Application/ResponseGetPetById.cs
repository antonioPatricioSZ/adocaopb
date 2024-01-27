using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;

namespace AdocaoPB.Application;

public class ResponseGetPetById {

    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public string Color { get; set; }
    public string Breed { get; set; }
    public string? Observations { get; set; }
    public bool IsAvailable { get; set; }
    public ResponseUser Owner { get; set; }

}
