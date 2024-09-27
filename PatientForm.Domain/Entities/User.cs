namespace PatientForm.Domain.Entities;

public record User(string Username,
                   string Password,
                   string FirstName,
                   string LastName);