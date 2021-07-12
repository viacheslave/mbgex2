using System;
using System.Threading.Tasks;

namespace mbgex2
{
	class Program
  {
    async static Task Main(string[] args)
    {
      // get auth cookies
      var authCookies = await new AuthClient()
        .GetAuthCookies(
          new UserCredentials(args[0], args[1]));

      // get data
      var data = await new DataProvider(authCookies)
        .Grab(
          new Dates(
            DateTime.Parse(args[2]),
            DateTime.Parse(args[3])));

      // export
      await new DataExporter().SaveRaw(data);

      new DataExporter().SaveByUtility(data);

      Console.WriteLine("All Done.");
    }
  }
}
