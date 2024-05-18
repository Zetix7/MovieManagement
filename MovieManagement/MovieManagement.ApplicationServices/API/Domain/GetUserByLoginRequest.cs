using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetUserByLoginRequest : RequestBase, IRequest<GetUserByLoginResponse>
{
    public string? Login { get; set; }
}
