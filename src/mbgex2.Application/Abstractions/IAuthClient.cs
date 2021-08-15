using System.Threading.Tasks;

namespace mbgex2.Application
{
	public interface IAuthClient
	{
		Task<AuthCookies> GetAuthCookies(UserCredentials credentials);
	}
}