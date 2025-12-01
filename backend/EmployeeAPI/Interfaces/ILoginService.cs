using System;
using EmployeeAPI.Data.Entities;
using EmployeeAPI.Data.Models;

namespace EmployeeAPI.Interfaces;

public interface ILoginService
{
    public OpResult<User> LogIn(string username, string Password);
    public OpResult<User> CheckPrivilage(string id);
    public OpResult<User> Register(User newUser);
}
