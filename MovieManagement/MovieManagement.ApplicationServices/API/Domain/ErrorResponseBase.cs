﻿namespace MovieManagement.ApplicationServices.API.Domain;

public abstract class ErrorResponseBase
{
    public ErrorModel? Error { get; set; }
}
