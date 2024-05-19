namespace MovieManagement.ApplicationServices.API.Domain;

public abstract class RequestBase
{
    public string? UsernameAuthentication { get; set; }
    public string? AccessLevelAuthentication { get; set; }
    public bool IsActiveAuthentication { get; set; }
}
