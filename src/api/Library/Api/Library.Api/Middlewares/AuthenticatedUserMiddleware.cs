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
                var userId = context.User.FindFirst("user_id")?.Value;
                var userRole = context.User.FindFirst("user_role")?.Value;

                // Możesz tutaj zalogować, przekazać dalej, sprawdzić uprawnienia itp.
                Console.WriteLine($"Użytkownik ID: {userId}, Rola: {userRole}");
            }

            await _next(context);
        }
    }
}
