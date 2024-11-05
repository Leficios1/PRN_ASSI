using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BO.Models;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using PRN_ASS1_RazorPage.Helper;

namespace PRN_ASS1_RazorPage.Pages.Jewely
{
    public class IndexModel : AuthenticationHelper
    {

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration)
        {
        }

        public List<SilverJewelry> SilverJewelry { get; set; } = new();
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {

                //RegisAuthen
                if (!SetupTokenAuthentication())
                {
                    RedirectToPage("/Index");
                }


                // Gọi API
                var response = await _httpClient.GetAsync("http://localhost:5113/api/Jwerly/GetAll");

                if (response.IsSuccessStatusCode)
                {

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await response.Content.ReadAsStringAsync();
                    SilverJewelry = JsonSerializer.Deserialize<List<SilverJewelry>>(content, options);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    HttpContext.Session.Remove("Token");
                    RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = $"Failed to load data: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }
    }
}
