namespace MovieManagement.ApplicationServices.API.Domain;

public abstract class RequestBase
{
    public string? LoginAuthentication { get; set; }
    public string? AccessLevelAuthentication { get; set; }
    public bool IsActiveAuthentication { get; set; }
}
