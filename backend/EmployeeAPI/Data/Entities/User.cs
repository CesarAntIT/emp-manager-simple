using System;
using Microsoft.AspNetCore.Identity;

namespace EmployeeAPI.Data.Entities;

public class User
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
