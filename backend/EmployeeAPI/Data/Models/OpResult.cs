using System;

namespace EmployeeAPI.Data.Models;

public class OpResult<T>
{
    string message { get; set; }
    bool success { get; set; }
    T? Value { get; set; }
}
