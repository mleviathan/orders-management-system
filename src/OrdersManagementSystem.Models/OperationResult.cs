namespace OrdersManagementSystem.Models;

/// <summary>
/// This class represent the result of an operation on database (e.g. Create, Update, Delete)
/// </summary>
public class OperationResult<T>
{
    public string Message { get; set; }

    public T Value { get; set; }
}