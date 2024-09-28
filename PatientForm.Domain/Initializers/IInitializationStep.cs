namespace PatientForm.Domain.Initializers;

public interface IInitializationStep
{
    Task Execute();
}