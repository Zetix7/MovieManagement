﻿namespace MovieManagement.DataAccess.Entities;

public class User : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
