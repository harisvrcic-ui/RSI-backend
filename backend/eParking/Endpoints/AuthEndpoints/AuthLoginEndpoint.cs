using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static eParking.Endpoints.AuthEndpoints.AuthLoginEndpoint;

namespace eParking.Endpoints.AuthEndpoints;

[Route(ApiRouteConstants.Auth)]
public class AuthLoginEndpoint(ApplicationDbContext db, IMyAuthService authService) : MyEndpointBaseAsync
    .WithRequest<LoginRequest>
    .WithActionResult<LoginResponse>
{
    [HttpPost(ApiRouteConstants.Login)]
    public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        // Check if user exists in database
        var loggedInUser = await db.MyAppUsers
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (loggedInUser == null || !loggedInUser.VerifyPassword(request.Password))
        {
            // Save changes only if user existed and failed login attempts were incremented
            if (loggedInUser != null)
            {
                await db.SaveChangesAsync(cancellationToken);
            }
            return Unauthorized(new { Message = "Incorrect username or password" });
        }

        // Generisanje novog autentifikacionog tokena
        MyAuthenticationToken newAuthToken = await authService.GenerateSaveAuthToken(loggedInUser, cancellationToken);
        MyAuthInfo authInfo = authService.GetAuthInfoFromTokenModel(newAuthToken);

        return Ok(new LoginResponse
        {
            Token = newAuthToken.Value,
            MyAuthInfo = authInfo
        });

    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponse
    {
        public required MyAuthInfo? MyAuthInfo { get; set; }
        public string Token { get; internal set; }
    }

    /*
    Custom auth vs JWT (JSON Web Token) – main differences:

    1. Token structure: Custom uses a random string as DB lookup key; user data is loaded from server. JWT is
    self-contained (header.payload.signature) with encoded user claims and a signature for integrity.

    2. Verification: Custom requires a DB query per request (MyAuthenticationTokens). JWT can be verified without
    DB access by validating the signature, reducing load.

    3. Expiry: Custom needs manual expiry (e.g. RecordedAt/ExpiresAt) and cleanup. JWT has exp claim for automatic
    expiry without server-side storage.

    4. Security: Custom token is a reference only; if intercepted, account access is possible while valid. JWT uses
    cryptographic signing (e.g. HMAC/RSA) but is harder to revoke once issued.

    5. Revocation: Custom – delete token in DB to revoke. JWT – no built-in revocation; needs a revocation list or
    short-lived + refresh tokens.

    6. Performance: Custom hits DB every request. JWT avoids DB for basic verification, better for scale.

    Conclusion: For frequent revocation and smaller apps, custom can be simpler. For larger, scalable systems, JWT
    is often better. For learning, custom is easier to understand (token generation, DB storage, validation) and
    prepares for JWT later.
     */

}
