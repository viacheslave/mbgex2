using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace mbgex2
{
	internal sealed class AuthClient
	{
    /// <summary>
    ///   Gets auth cookies
    /// </summary>
    /// <param name="credentials">User credentials (login/password)</param>
    /// <returns>Credentials DTOs</returns>
		public async Task<AuthCookies> GetAuthCookies(UserCredentials credentials)
		{
			if (credentials is null)
				throw new ArgumentNullException(nameof(credentials));

      Logger.Out($"Logging in as {credentials.Login}");

      var response = await new FlurlRequest(new Url("https://erc.megabank.ua/ua/node?destination=node"))
        .WithAutoRedirect(false)
        .PostUrlEncodedAsync(new
        {
          name = credentials.Login,
          pass = credentials.Password,
          op = "Вхід",
          form_id = "user_login_block"
        });

      return new AuthCookies
      {
        Cookies = response.Cookies
          .Select(c => (c.Name, c.Value))
          .ToList()
      };
    }
	}
}
