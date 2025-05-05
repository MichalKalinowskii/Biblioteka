using Library.Domain.Locations.Errors;
using Library.Domain.SeedWork;

namespace Library.Domain.Locations.Models;

public class Location
{
    public Guid Id { get; set; }
    public int Zone { get; set; }
    public int Shell { get; set; }
    public int Level { get; set; }
    public string LocationCode { get; set; }
    public string Description { get; set; }

    public Location(Guid id, int zone, int shell, int level, string description)
    {
        Id = id;
        Zone = zone;
        Shell = shell;
        Level = level;
        Description = description;
    }

    public Result ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Result.Failure(LocationErrors.InvalidLocationDescription);
        }

        return Result.Success();
    }

}