namespace AdocaoPB.Domain.Entities;

public class BaseEntity {

    public DateTime CreationDate { get; set; } = TimeZoneInfo
        .ConvertTimeFromUtc(
            DateTime.UtcNow, 
            TimeZoneInfo.FindSystemTimeZoneById(
                "E. South America Standard Time"
            )
        );
    
}
