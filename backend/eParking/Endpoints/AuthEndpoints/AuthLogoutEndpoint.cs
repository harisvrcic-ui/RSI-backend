using Azure.Core;
using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static eParking.Endpoints.AuthEndpoints.AuthLogoutEndpoint;

namespace eParking.Endpoints.AuthEndpoints;

[Route(ApiRouteConstants.Auth)]
[MyAuthorization(isAdmin: false, isUser: false)]
public class AuthLogoutEndpoint(ApplicationDbContext db, IMyAuthService authService) : MyEndpointBaseAsync
    .WithoutRequest
    .WithResult<LogoutResponse>
{
    [HttpPost(ApiRouteConstants.Logout)]
    public override async Task<LogoutResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Get token from header
            string? authToken = Request.Headers["my-auth-token"];

            if (string.IsNullOrEmpty(authToken))
            {
                return new LogoutResponse
                {
                    IsSuccess = false,
                    Message = "Token is missing in the request header."
                };
            }

            // Attempt token revocation
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
