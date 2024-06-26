﻿using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetUsersRequest : RequestBase, IRequest<GetUsersResponse>
{
}
