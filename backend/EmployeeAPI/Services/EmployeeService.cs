using System;
using EmployeeAPI.Data;
using EmployeeAPI.Data.Context;
using EmployeeAPI.Data.Entities;

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

    public Employee Add(Employee emp)
    {
        if (emp == null)
            return null!;

        if (string.IsNullOrWhiteSpace(emp.FirstName))
            return null!;
        if (string.IsNullOrWhiteSpace(emp.LastName))
            return null!;
        if (DateTime.Compare(emp.BirthDate, new DateTime(2008, 1, 1)) > 0)
            return null!;
        if (emp.Department!.Length > 2 || string.IsNullOrWhiteSpace(emp.Department))
            return null!;
        if (emp.Pay < 0)
            return null!;

        emp.ID = Guid.NewGuid();
        emp.HireDate = DateTime.Now;

        _context.Employees.Add(emp);
        _context.SaveChanges();

        return emp;
    }
    public bool Remove(Guid ID)
    {
        var emp = GetById(ID);
        if (emp == null)
            return false;
        _context.Employees.Remove(emp);
        _context.SaveChanges();
        return true;
    }

    public Employee Edit(Guid Id, Employee emp)
    {
        if (emp == null)
            return null!;

        var UpdEmp = GetById(Id);
        if (UpdEmp == null)
            return UpdEmp!;
        if (string.IsNullOrWhiteSpace(emp.FirstName))
            return null!;
        if (string.IsNullOrWhiteSpace(emp.LastName))
            return null!;
        if (DateTime.Compare(emp.BirthDate, new DateTime(2008, 1, 1)) > 0)
            return null!;
        if (emp.Department!.Length > 2 || string.IsNullOrWhiteSpace(emp.Department))
            return null!;
        if (DateTime.Compare(emp.HireDate, DateTime.Now) > 0)
            return null!;
        if (emp.Pay < 0)
            return null!;

        UpdEmp.LastName = emp.LastName;
        UpdEmp.FirstName = emp.FirstName;
        UpdEmp.BirthDate = emp.BirthDate;
        UpdEmp.Department = emp.Department;
        UpdEmp.HireDate = emp.HireDate;
        UpdEmp.Pay = emp.Pay;

        _context.Update(UpdEmp);
        _context.SaveChanges();
        return UpdEmp;
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
