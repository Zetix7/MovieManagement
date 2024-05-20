using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetUserByUsernameRequest : RequestBase, IRequest<GetUserByUsernameResponse>
{
    public string? Username { get; set; }
}
