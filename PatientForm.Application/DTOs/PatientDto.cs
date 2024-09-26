using PatientForm.Domain.Entities;

namespace PatientForm.Application.DTOs;

public record PatientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

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