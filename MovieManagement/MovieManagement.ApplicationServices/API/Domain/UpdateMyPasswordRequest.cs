using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class UpdateMyPasswordRequest : RequestBase, IRequest<UpdateMyPasswordResponse>
{
    public string? Password { get; set; }
}
