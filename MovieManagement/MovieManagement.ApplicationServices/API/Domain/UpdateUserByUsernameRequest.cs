using MediatR;
using static MovieManagement.DataAccess.Entities.User;

namespace MovieManagement.ApplicationServices.API.Domain;

public class UpdateUserByUsernameRequest : RequestBase, IRequest<UpdateUserByUsernameResponse>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Role AccessLevel { get; set; }
    public bool IsActive { get; set; }
}
