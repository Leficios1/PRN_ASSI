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
using System.Configuration;

namespace PRN_ASS1_RazorPage.Pages.Jewely
{
    public class DeleteModel : AuthenticationHelper
    {

        public DeleteModel(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration)
        {
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;
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
                    var content = await response.Content.ReadAsStringAsync();
                    SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Lấy thông tin Category nếu cần
                    if (!string.IsNullOrEmpty(SilverJewelry.CategoryId))
                    {
                        var categoryResponse = await _httpClient.GetAsync($"http://localhost:5113/api/Category/{SilverJewelry.CategoryId}");
                        if (categoryResponse.IsSuccessStatusCode)
                        {
                            var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                            SilverJewelry.Category = JsonSerializer.Deserialize<Category>(categoryContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }

                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5113/api/Jwerly/Deleted/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorMessage = $"Failed to delete jewelry: {await response.Content.ReadAsStringAsync()}";
                    return Page();
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
