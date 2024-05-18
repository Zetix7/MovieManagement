using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetMeRequest : RequestBase, IRequest<GetMeResponse>
{
}
