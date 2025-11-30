using System;
using EmployeeAPI.Data.Entities;

namespace EmployeeAPI.Services;

public interface IEmployeeService
{
    public IEnumerable<Employee> Get();
    public Employee GetById(Guid id);
    public Employee Add(Employee emp);
    public bool Remove(Guid ID);
    public Employee Edit(Guid id, Employee emp);
    public List<string> GetDepartments();
}
