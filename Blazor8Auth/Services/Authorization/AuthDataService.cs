using System.Security.Claims;
using Blazor8Auth.Database.Contexts;
using Blazor8Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Blazor8Auth.Services.Interfaces;

namespace Blazor8Auth.Services.Authorization
{

    public class AuthDataService(AppDbContext dbContext, IPasswordHasher passwordHasher) : IAuthDataService
    {
        public async Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ServiceResponse<ClaimsPrincipal>("Email is required");
            }

            if (string.IsNullOrEmpty(password))
            {
                return new ServiceResponse<ClaimsPrincipal>("Password is required");
            }

            try
            {
                var user = await dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email); if (user == null)
                {
                    return new ServiceResponse<ClaimsPrincipal>(
                        "Unknown user");
                }

                if (!passwordHasher.Verify(password, user.PasswordHash))
                {
                    return new ServiceResponse<ClaimsPrincipal>("Invalid password");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, user.Role.UserRole)
                };

                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);

                return new ServiceResponse<ClaimsPrincipal>("Login successful", true, principal);
            }
            catch
            {
                return new ServiceResponse<ClaimsPrincipal>("Something went wrong.");
            }
        }
    }
}
