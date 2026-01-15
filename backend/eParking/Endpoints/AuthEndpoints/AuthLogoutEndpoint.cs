using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static eParking.Endpoints.AuthEndpoints.AuthLogoutEndpoint;
using eParking.Data;
using eParking.Helper.Api;
using eParking.Services;

namespace eParking.Endpoints.AuthEndpoints;

[Route("auth")]
public class AuthLogoutEndpoint(ApplicationDbContext db, IMyAuthService authService) : MyEndpointBaseAsync
    .WithoutRequest
    .WithResult<LogoutResponse>
{
    [HttpPost("logout")]
    public override async Task<LogoutResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Dohvatanje tokena iz headera
            string? authToken = Request.Headers["my-auth-token"];

            if (string.IsNullOrEmpty(authToken))
            {
                return new LogoutResponse
                {
                    IsSuccess = false,
                    Message = "Token is missing in the request header."
                };
            }

            // Pokušaj revokacije tokena
            bool isRevoked = await authService.RevokeAuthToken(authToken, cancellationToken);

            return new LogoutResponse
            {
                IsSuccess = isRevoked,
                Message = isRevoked ? "Logout successful." : "Invalid token or already logged out."
            };
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            Console.WriteLine($"Error during logout: {ex.Message}");
            
            return new LogoutResponse
            {
                IsSuccess = false,
                Message = "An error occurred during logout."
            };
        }
    }

    public class LogoutResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
