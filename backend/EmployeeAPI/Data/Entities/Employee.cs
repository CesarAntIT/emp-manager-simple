using System;

namespace EmployeeAPI.Data.Entities;

public class Employee
{
    public Guid ID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Pay { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string? Department { get; set; }
}
