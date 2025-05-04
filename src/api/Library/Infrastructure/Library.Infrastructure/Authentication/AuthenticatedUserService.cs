using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Authentication
{
    internal class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal? _user;

        public AuthenticatedUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _user = _contextAccessor.HttpContext?.User;
        }

        public string? Role => _user?.FindFirst(ClaimTypes.Role)?.Value;

        public Guid? LibraryCardId {
            get
            {
                return Guid.TryParse(_user?.FindFirst("libraryCard_id")?.Value, out var guid) ? guid : (Guid?)null;
            }
        }

        
        public Guid? UserId
        {
            get
            {
                return Guid.TryParse(_user?.FindFirst("user_id")?.Value, out var guid) ? guid : (Guid?)null;
            }
        }

        public string? GetClaim(string claimType)
        {
            return _user?.FindFirst(claimType)?.Value;
        }
    }
}
