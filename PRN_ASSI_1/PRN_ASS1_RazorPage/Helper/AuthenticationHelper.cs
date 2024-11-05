using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace PRN_ASS1_RazorPage.Helper
{
    public abstract class AuthenticationHelper : PageModel
    {
        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;

        protected AuthenticationHelper(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient();
            _configuration = configuration;
        }

        protected bool SetupTokenAuthentication()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}
