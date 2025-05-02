using System.Security.Claims;

namespace Library.Api.Middlewares
{
    public class AuthenticatedUserMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticatedUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = context.User.FindFirst(ClaimTypes.Role)?.Value;

                // Możesz tutaj zalogować, przekazać dalej, sprawdzić uprawnienia itp.
                Console.WriteLine($"Użytkownik ID: {userId}, Email: {email}");
            }

            await _next(context);
        }
    }
}
