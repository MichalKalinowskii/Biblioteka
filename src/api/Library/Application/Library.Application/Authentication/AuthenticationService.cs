using Library.Application.Authentication.Login;
using Library.Application.Authentication.Register;
using Library.Domain.SeedWork;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Authentication;

public class AuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenService _jwtTokenService;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<Result> RegisterAsync(RegisterRequestDto? registerRequestDto,
        CancellationToken cancellationToken)
    {
        if (registerRequestDto is null)
        {
            return Result.Failure(AuthenticationErrors.IncompleteUserData());
        }

        if (string.IsNullOrWhiteSpace(registerRequestDto.Email) ||
            string.IsNullOrWhiteSpace(registerRequestDto.FirstName) ||
            string.IsNullOrWhiteSpace(registerRequestDto.LastName) ||
            string.IsNullOrWhiteSpace(registerRequestDto.Password))
        {
            return Result.Failure(AuthenticationErrors.IncompleteUserData());
        }

        ApplicationUser applicationUser = new()
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName
        };

        var creationResult = await _userManager.CreateAsync(applicationUser, registerRequestDto.Password);

        if (!creationResult.Succeeded)
        {
            return Result.Failure(AuthenticationErrors.FailedUserCreation(creationResult.Errors.Select(x => x.Description).ToArray()));
        }

        return Result.Success();
    }
    
    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto? loginRequestDto, CancellationToken cancellationToken)
    {
        if (loginRequestDto is null)
        {
            return Result<LoginResponseDto>.Failure(AuthenticationErrors.IncompleteUserData());
        }

        if (string.IsNullOrWhiteSpace(loginRequestDto.Email) ||
            string.IsNullOrWhiteSpace(loginRequestDto.Password))
        {
            return Result<LoginResponseDto>.Failure(AuthenticationErrors.IncompleteUserData());
        }

        ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(loginRequestDto.Email);

        if (applicationUser is null)
        {
            return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidEmailOrPassword());
        }

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequestDto.Password, false);

        if (!signInResult.Succeeded)
        {
            return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidEmailOrPassword());
        }

        string token = _jwtTokenService.GenerateToken(applicationUser);

        return Result<LoginResponseDto>.Success(new LoginResponseDto(token));
    }
}