namespace Library.Application.Authentication.Register;

public record RegisterRequestDto(string? Email, string? Password, string? FirstName, string? LastName);