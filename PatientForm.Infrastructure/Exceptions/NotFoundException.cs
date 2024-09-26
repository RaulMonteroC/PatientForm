namespace PatientForm.Infrastructure.Exceptions;

public class NotFoundException(string resourceType, string id)
    : Exception($"{resourceType} with id {id} doesn't exist");