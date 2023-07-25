using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DocumentApi.Filters
{
    public class AuthFilter: IAuthorizationFilter
    {
        private const string ValidUserName = "vs";
        private const string ValidPassword = "rekrutacja";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Extract the credentials from the Authorization header
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
                string decodedCredentials = Encoding.UTF8.GetString(decodedBytes);
                string[] credentials = decodedCredentials.Split(new[] { ':' }, 2);

                // Check if the provided username and password are valid
                if (credentials.Length == 2 &&
                    credentials[0] == ValidUserName &&
                    credentials[1] == ValidPassword)
                {
                    // Valid credentials, allow access
                    return;
                }
            }

            // Invalid credentials, return Unauthorized status
            context.Result = new UnauthorizedResult();
        }
    }
}
