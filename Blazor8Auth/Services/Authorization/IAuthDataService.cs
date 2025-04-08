using Blazor8Auth.Entities;
using System.Security.Claims;

namespace Blazor8Auth.Services.Authorization
{
    public interface IAuthDataService
    {
        Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password);
    }
}
