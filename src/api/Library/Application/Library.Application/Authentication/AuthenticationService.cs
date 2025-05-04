using Library.Application.Authentication.Login;
using Library.Application.Authentication.Register;
using Library.Domain.Clients;
using Library.Domain.SeedWork;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Authentication;

public class AuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IClientRepository _clientRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ClientService _clientService;
    private readonly JwtTokenService _jwtTokenService;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
        IClientRepository clientRepository,
        SignInManager<ApplicationUser> signInManager,
        ClientService clientService,
        JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _clientRepository = clientRepository;
        _signInManager = signInManager;
        _clientService = clientService;
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
        
        await _clientService.AddAsync(applicationUser.Id, cancellationToken);

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

        IList<string> roles = await _userManager.GetRolesAsync(applicationUser);

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequestDto.Password, false);

        if (!signInResult.Succeeded)
        {
            return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidEmailOrPassword());
        }
        
        string userRole = roles.First();

        string token = string.Empty;
        
        if (userRole == "Client")
        {
            var client = await _clientRepository.GetByIdAsync(applicationUser.Id, cancellationToken);
            
            token = _jwtTokenService.GenerateClientToken(applicationUser, userRole, client.LibraryCardId);
        }
        else
        {
            token = _jwtTokenService.GenerateToken(applicationUser, userRole);
        }
        

        return Result<LoginResponseDto>.Success(new LoginResponseDto(token));
    }
}