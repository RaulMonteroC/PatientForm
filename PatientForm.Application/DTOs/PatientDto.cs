using PatientForm.Domain.Entities;

namespace PatientForm.Application.DTOs;

public record PatientDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    public Patient ToEntity() => new(Id,
                                     Name,
                                     LastName,
                                     PhoneNumber,
                                     Email);

    public static PatientDto FromEntity(Patient entity)
        => new()
           {
               Id = entity.Id,
               Name = entity.Name,
               LastName = entity.LastName,
               PhoneNumber = entity.PhoneNumber,
               Email = entity.Email
           };
}