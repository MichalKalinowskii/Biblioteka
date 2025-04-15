using Library.Domain.SeedWork;

namespace Library.Domain.Staff;

public class Employee
{
    public int Id { get; private set; }
    
    public Guid UserId { get; private set; }

    internal Employee(Guid userId)
    {
        UserId = userId;
    }

    public static Result<Employee> Create(Guid userId)
    {
        return Result<Employee>.Success(new Employee(userId));
    }
}