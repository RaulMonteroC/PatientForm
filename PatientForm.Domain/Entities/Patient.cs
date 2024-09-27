namespace PatientForm.Domain.Entities;

public record Patient(string Id,
                      string Name,
                      string LastName,
                      string? PhoneNumber,
                      string? Email)
{
    public Guid InsuranceId { get; set; }
    public Insurance? Insurance { get; set; }
}