using System;
using EmployeeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data;

public class EmployeeDB : DbContext
{   public EmployeeDB(DbContextOptions<EmployeeDB> dbContextOptions) : base(dbContextOptions)
    {

    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
}
