using System.Net.Http.Headers;
using Microsoft.Graph;

namespace Microsoft_Graph_SDK_ASPNET_Connnect.Controllers
{
	public class SDKHelper : Controller
	{
		private static GraphServiceClient graphclient = null;
		
		public static GraphServiceClient GetAuthenticatedClient()
		{
			GraphServiceClient graphClient = new GraphServiceClient(
				new DelegateAuthenticationProvider(
					async (requestMessage) =>
					{
						string accessToken = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
						
						requestMessage.Header.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
					}));
			return graphClient;
		}
		
		public static void SignOutClient()
		{
			graphClient = null;
		}
	}
}
