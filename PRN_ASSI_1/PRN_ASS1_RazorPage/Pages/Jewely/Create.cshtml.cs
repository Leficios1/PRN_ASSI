using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BO.Models;
using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Net;
using PRN_ASS1_RazorPage.Helper;
using System.Configuration;

namespace PRN_ASS1_RazorPage.Pages.Jewely
{
    public class CreateModel : AuthenticationHelper
    {

        public CreateModel(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration)
        {
        }
        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (!SetupTokenAuthentication())
                {
                    RedirectToPage("/Index");
                }

                var response = await _httpClient.GetAsync("http://localhost:5113/api/Category/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<Category>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewData["CategoryId"] = new SelectList(
                        categories,
                        nameof(Category.CategoryId),
                        nameof(Category.CategoryName)
                    );
                }

                else
                {
                    ErrorMessage = "Failed to load categories.";
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
                // Reload categories if validation fails
                var response = await _httpClient.GetAsync("http://localhost:5113/api/Category/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<Category>>(content,
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

            try
            {
                var content = JsonSerializer.Serialize(SilverJewelry);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5113/api/Jwerly/Create", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorMessage = $"Failed to create jewelry: {await response.Content.ReadAsStringAsync()}";

                    // Reload categories
                    var categoryResponse = await _httpClient.GetAsync("http://localhost:5113/api/Category/GetAll");
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
