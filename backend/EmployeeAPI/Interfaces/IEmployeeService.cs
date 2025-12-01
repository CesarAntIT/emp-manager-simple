using System;
using EmployeeAPI.Data.Entities;
using EmployeeAPI.Data.Models;

namespace EmployeeAPI.Services;

public interface IEmployeeService
{
    public IEnumerable<Employee> Get();
    public Employee GetById(Guid id);
    public OpResult<Employee> Add(Employee emp);
    public OpResult<Employee> Remove(Guid ID);
    public OpResult<Employee> Edit(Guid id, Employee emp);
    public List<string> GetDepartments();
}
