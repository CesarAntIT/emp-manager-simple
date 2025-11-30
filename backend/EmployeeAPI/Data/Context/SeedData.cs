using System;
using System.ComponentModel.DataAnnotations;
using EmployeeAPI.Data.Entities;

namespace EmployeeAPI.Data.Context;

public class SeedData
{
    public static void SetSeedData(EmployeeDB _context)
    {
        if (_context == null)
            return;

        if (_context.Employees.Count() <= 0)
        {
            _context.Employees.Add(new Employee()
            {
                FirstName = "César",
                LastName = "Aybar",
                Department = "IT",
                HireDate = new DateTime(2020, 11, 23),
                BirthDate = new DateTime(2005, 09, 14),
                Pay = 500m,
                ID = Guid.NewGuid(),
            });
            _context.Employees.Add(new Employee()
            {
                FirstName = "Diego",
                LastName = "Rodriguez",
                Department = "MK",
                HireDate = new DateTime(2018, 11, 23),
                BirthDate = new DateTime(2003, 10, 13),
                Pay = 450m,
                ID = Guid.NewGuid(),
            });

            _context.SaveChanges();
        }
    }
}
