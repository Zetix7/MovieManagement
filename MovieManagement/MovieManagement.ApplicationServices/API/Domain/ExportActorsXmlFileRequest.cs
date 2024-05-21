﻿using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class ExportActorsXmlFileRequest : RequestBase, IRequest<ExportActorsXmlFileResponse>
{
}
