namespace PatientForm.Domain.Entities;

public record Insurance(Guid Id,
                        string Number,
                        string Company,
                        DateOnly ExpirationDate,
                        string PhotoUrl);