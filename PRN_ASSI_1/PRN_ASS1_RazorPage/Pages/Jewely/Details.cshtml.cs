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
using System.Net;
using PRN_ASS1_RazorPage.Helper;

namespace PRN_ASS1_RazorPage.Pages.Jewely
{
    public class DetailsModel : AuthenticationHelper
    {
        public DetailsModel(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration)
        {
        }

        public SilverJewelry SilverJewelry { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            if (!SetupTokenAuthentication())
            {
                RedirectToPage("/Index");
            }
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5113/api/Jwerly/GetById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await response.Content.ReadAsStringAsync();
                    SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(content, options);

                    // Nếu muốn lấy thêm thông tin Category
                    if (!string.IsNullOrEmpty(SilverJewelry.CategoryId))
                    {
                        var categoryResponse = await _httpClient.GetAsync($"http://localhost:5113/api/Category/{SilverJewelry.CategoryId}");
                        if (categoryResponse.IsSuccessStatusCode)
                        {
                            var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                            SilverJewelry.Category = JsonSerializer.Deserialize<Category>(categoryContent, options);
                        }
                    }

                    return Page();
                }
                else
                {
                    ErrorMessage = "Failed to load jewelry details.";
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                return Page();
            }
        }
    }
}
