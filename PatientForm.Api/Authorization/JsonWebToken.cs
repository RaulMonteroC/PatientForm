namespace PatientForm.Api.Authorization;

public class JsonWebToken
{
    public string? AccessToken { get; set; }

    public int ExpiresIn { get; set; }
}