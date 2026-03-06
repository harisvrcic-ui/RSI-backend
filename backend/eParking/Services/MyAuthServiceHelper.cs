using System;
using System.Text.Json.Serialization;
using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using Microsoft.EntityFrameworkCore;

namespace eParking.Services
{
    public class MyAuthServiceHelper
        //(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, MyTokenGenerator myTokenGenerator)
    {

        // Generate new token for user
        public static async Task<MyAuthenticationToken> GenerateSaveAuthToken(string? IpAddress, ApplicationDbContext applicationDbContext, MyAppUser user, CancellationToken cancellationToken = default)
        {
            string randomToken = MyTokenGenerator.Generate(10);

            var authToken = new MyAuthenticationToken
            {
                IpAddress = IpAddress ?? string.Empty,
                Value = randomToken,
                MyAppUser = user,
                RecordedAt = DateTime.UtcNow,
            };

            applicationDbContext.Add(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return authToken;
        }

        // Remove token from database
        public static async Task<bool> RevokeAuthToken(ApplicationDbContext applicationDbContext, string tokenValue, CancellationToken cancellationToken = default)
        {
            var authToken = await applicationDbContext.MyAuthenticationTokens
                .FirstOrDefaultAsync(t => t.Value == tokenValue, cancellationToken);

            if (authToken == null)
                return false;

            applicationDbContext.Remove(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Dohvatanje informacija o autentifikaciji korisnika
        public static MyAuthInfo GetAuthInfoFromTokenString(ApplicationDbContext applicationDbContext, string? authToken)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return GetAuthInfoFromTokenModel(null);
            }

            MyAuthenticationToken? myAuthToken = applicationDbContext.MyAuthenticationTokens
                .IgnoreQueryFilters()
                .Include(t => t.MyAppUser)
                .SingleOrDefault(x => x.Value == authToken);

            return GetAuthInfoFromTokenModel(myAuthToken);
        }


        // Dohvatanje informacija o autentifikaciji korisnika
        public static MyAuthInfo GetAuthInfoFromRequest(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            string? authToken = httpContextAccessor.HttpContext?.Request.Headers["my-auth-token"];
            return GetAuthInfoFromTokenString(applicationDbContext, authToken);
        }

        public static MyAuthInfo GetAuthInfoFromTokenModel(MyAuthenticationToken? myAuthToken)
        {
            if (myAuthToken == null || myAuthToken.MyAppUser == null)
            {
                return new MyAuthInfo
                {
                    IsAdmin = false,
                    IsUser = false,
                    IsLoggedIn = false,
                };
            }

            var u = myAuthToken.MyAppUser;
            return new MyAuthInfo
            {
                UserId = myAuthToken.MyAppUserId,
                Email = u.Email ?? string.Empty,
                FirstName = u.FirstName ?? string.Empty,
                LastName = u.LastName ?? string.Empty,
                IsAdmin = u.IsAdmin,
                IsUser = u.IsUser,
                IsLoggedIn = true,
                SlikaPath = string.Empty,
            };
        }
    }

    // DTO to hold authentication information
    public class MyAuthInfo
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public bool IsLoggedIn { get; set; }
        public string SlikaPath {  get; set; }
    }
}
