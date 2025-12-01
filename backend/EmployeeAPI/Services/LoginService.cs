using System;
using System.Text.RegularExpressions;
using EmployeeAPI.Data;
using EmployeeAPI.Data.Context;
using EmployeeAPI.Data.Entities;
using EmployeeAPI.Data.Models;
using EmployeeAPI.Interfaces;

namespace EmployeeAPI.Services;

public class LoginService : ILoginService
{
    EmployeeDB _context = null!;
    static bool isDbfresh = true;
    public LoginService(EmployeeDB db)
    {
        _context = db;
        if (_context.Employees.Count() <=0 && _context.Users.Count() <= 0 && isDbfresh)
        {
            SeedData.SetSeedData(_context);
            isDbfresh = false;
        }
    }

    public OpResult<User> LogIn(string username, string Password)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == username);
        if (user == null)
            return new OpResult<User>(false, user!, "A user by this username does not exist");
        if (user.Password != Password)
            return new OpResult<User>(false, null!, "User password is incorrect, please try again");
        return new OpResult<User>(true, user, "Succesfully Logged In");
    }
    public OpResult<User> CheckPrivilage(string id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return new OpResult<User>(false, user!, "Current user does not exist");
        if (user.Role != "Admin")
            return new OpResult<User>(false, user!, "Current user does not have proper privilages");
        return new OpResult<User>(true, user!, "Privilage Confirmed");
    }
    public OpResult<User> Register(User newUser)
    {
        if (string.IsNullOrWhiteSpace(newUser.Username))
            return new OpResult<User>(false, newUser, "Username cannot be a blank input");
        if (newUser.Username != newUser.Username.Replace(" ", ""))
            return new OpResult<User>(false, newUser, "Usernamen cannot have spaces");
        if (Regex.IsMatch(newUser.Username, @"^[a-zA-Z]+$"))
            return new OpResult<User>(false, newUser, "Usernamen can only be letters");

        if (string.IsNullOrWhiteSpace(newUser.Password))
            return new OpResult<User>(false, newUser, "Username cannot be a blank input");

        newUser.ID = Guid.NewGuid();
        _context.Users.Add(newUser);
        _context.SaveChanges();
        
        return new OpResult<User>(true, newUser, "User registered successfully");
    }


}
