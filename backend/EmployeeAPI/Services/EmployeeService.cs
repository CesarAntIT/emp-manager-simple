using System;
using EmployeeAPI.Data;
using EmployeeAPI.Data.Context;
using EmployeeAPI.Data.Entities;
using EmployeeAPI.Data.Models;

namespace EmployeeAPI.Services;

public class EmployeeService:IEmployeeService
{
    EmployeeDB _context { get; set; }
    public EmployeeService(EmployeeDB db)
    {
        _context = db;
        SeedData.SetSeedData(_context);
    }
    public IEnumerable<Employee> Get()
    {
        var empList = _context.Employees;
        if (empList != null)
            return empList;
        else
            return null!;
    }
    
    public Employee GetById(Guid id)
    {
        var emp = _context.Employees.FirstOrDefault(x => x.ID == id);
        if (emp == null)
            return null!;
        return emp;
    }

    public OpResult<Employee> Add(Employee emp)
    {
        if (emp == null)
            return null!;

        if (string.IsNullOrWhiteSpace(emp.FirstName))
            return new OpResult<Employee>(false, null!, "Firstname input cannot be empty");
        if (string.IsNullOrWhiteSpace(emp.LastName))
            return new OpResult<Employee>(false, null!, "Lastname input cannot be empty");
        if (DateTime.Compare(emp.BirthDate, new DateTime(2008, 1, 1)) > 0)
            return new OpResult<Employee>(false, null!, "Date of birth cannot be before 2008");
        if (emp.Department!.Length > 2 || string.IsNullOrWhiteSpace(emp.Department))
            return new OpResult<Employee>(false, null!, "Invalid Input for Department.\nMust be 2 character input");
        if (emp.Pay < 0)
            return null!;

        emp.ID = Guid.NewGuid();
        emp.HireDate = DateTime.Now;

        _context.Employees.Add(emp);
        _context.SaveChanges();

        return new OpResult<Employee>(true, emp, $"The employee '{emp.FirstName} {emp.LastName}' was successfully added");
    }
    public OpResult<Employee> Remove(Guid ID)
    {
        var emp = GetById(ID);
        if (emp == null)
            return new OpResult<Employee>(false, null!, "Could not find employee set for removal");
        _context.Employees.Remove(emp);
        _context.SaveChanges();
        return new OpResult<Employee>(true, emp, $"The employee '{emp.FirstName} {emp.LastName}' was successfully removed");;
    }

    public OpResult<Employee> Edit(Guid Id, Employee emp)
    {
        if (emp == null)
            return null!;

        var UpdEmp = GetById(Id);
        if (UpdEmp == null)
            return new OpResult<Employee>(false, null!, "Could not find employee in system");
        if (string.IsNullOrWhiteSpace(emp.FirstName))
            return new OpResult<Employee>(false, null!, "Firstname input cannot be empty");
        if (string.IsNullOrWhiteSpace(emp.LastName))
            return new OpResult<Employee>(false, null!, "Lastname input cannot be empty");
        if (DateTime.Compare(emp.BirthDate, new DateTime(2008, 1, 1)) > 0)
            return new OpResult<Employee>(false, null!, "Date of birth cannot be before 2008");
        if (emp.Department!.Length > 2 || string.IsNullOrWhiteSpace(emp.Department))
            return new OpResult<Employee>(false, null!, "Invalid Input for Department.\nMust be 2 character input");
        if (DateTime.Compare(emp.HireDate, DateTime.Now) > 0)
            return new OpResult<Employee>(false, null!, "Hire date of employee cannot be set in a future date");
        if (emp.Pay < 0)
            return new OpResult<Employee>(false, null!, "The salary of an employee cannot be below $0");

        UpdEmp.LastName = emp.LastName;
        UpdEmp.FirstName = emp.FirstName;
        UpdEmp.BirthDate = emp.BirthDate;
        UpdEmp.Department = emp.Department;
        UpdEmp.HireDate = emp.HireDate;
        UpdEmp.Pay = emp.Pay;

        _context.Update(UpdEmp);
        _context.SaveChanges();
        return new OpResult<Employee>(true, UpdEmp, $"The employee '{UpdEmp.FirstName} {UpdEmp.LastName}' was successfully updated");

    }
    
    public List<string> GetDepartments()
    {
        var deps = _context.Employees.Select(x => x.Department).Distinct().ToList();
        if (deps == null)
            return null!;
        else
            return deps!;
    }
}
