namespace PatientForm.Infrastructure.Exceptions;

public class NotFoundException(string text) : Exception(text)
{
    public NotFoundException(string resourceType, string id) : 
        this($"{resourceType} with id {id} doesn't exist")
    {
        
    }
}