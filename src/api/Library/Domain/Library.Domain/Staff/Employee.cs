namespace Library.Domain.Staff;

public class Employee
{
    public int Id { get; private set; }
    
    public int UserId { get; set; }

    internal Employee(int userId)
    {
        UserId = userId;
    }
}