using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeAPI.Data.Models;

public class OpResult<T>
{
    public OpResult( bool suc, T val, string mes){
        message = mes;
        Value = val;
        success = suc;
    }

    public string message { get; set; }
    public bool success { get; set; }
    public T? Value { get; set; }
}
