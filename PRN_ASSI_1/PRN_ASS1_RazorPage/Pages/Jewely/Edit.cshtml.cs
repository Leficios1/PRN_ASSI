using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BO.Models;
using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using PRN_ASS1_RazorPage.Helper;

namespace PRN_ASS1_RazorPage.Pages.Jewely
{
    public class EditModel : AuthenticationHelper
    {

        public EditModel(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration)
        {
        }
        [BindProperty]
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
                // Get Jewelry details
                var response = await _httpClient.GetAsync($"http://localhost:5113/api/Jwerly/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(content, options);
                }
                else
                {
                    return NotFound();
                }

                // Get Categories for dropdown
                var categoryResponse = await _httpClient.GetAsync("http://localhost:5113/api/Category/getAll");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewData["CategoryId"] = new SelectList(
                        categories,
                        nameof(Category.CategoryId),
                        nameof(Category.CategoryName),
                        SilverJewelry.CategoryId
                    );
                }

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var content = JsonSerializer.Serialize(SilverJewelry);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    $"http://localhost:5113/api/Jwerly/update",
                    httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorMessage = $"Failed to update: {response.StatusCode}";

                    // Reload categories for dropdown
                    var categoryResponse = await _httpClient.GetAsync("http://localhost:5113/api/Category/getAll");
                    if (categoryResponse.IsSuccessStatusCode)
                    {
                        var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                        var categories = JsonSerializer.Deserialize<List<Category>>(categoryContent,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        ViewData["CategoryId"] = new SelectList(
                            categories,
                            nameof(Category.CategoryId),
                            nameof(Category.CategoryName),
                            SilverJewelry.CategoryId
                        );
                    }

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
