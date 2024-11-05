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
        public SilverJewelry SilverJewelry { get; set; } = new SilverJewelry(); // Initialize
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Kiểm tra và return nếu không có token
                if (!SetupTokenAuthentication())
                {
                    return RedirectToPage("/Index");  // Thêm return
                }

                // Load categories
                var response = await _httpClient.GetAsync("http://localhost:5113/api/Category/GetAll");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    HttpContext.Session.Remove("Token");
                    return RedirectToPage("/Index");
                }

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
                    ErrorMessage = $"Failed to load categories: {response.StatusCode}";
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
            try
            {
                // Kiểm tra token trước khi xử lý post
                if (!SetupTokenAuthentication())
                {
                    return RedirectToPage("/Index");
                }

                if (!ModelState.IsValid)
                {
                    // Load categories again if validation fails
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

                // Set create date if needed
                SilverJewelry.CreatedDate = DateTime.Now;

                var content = JsonSerializer.Serialize(SilverJewelry);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5113/api/Jwerly/Create", httpContent);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    HttpContext.Session.Remove("Token");
                    return RedirectToPage("/Index");
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }

                // If create failed
                ErrorMessage = $"Failed to create jewelry: {response.StatusCode}";
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseContent))
                {
                    ErrorMessage += $" - {responseContent}";
                }

                // Reload categories
                var reloadCategoryResponse = await _httpClient.GetAsync("http://localhost:5113/api/Category/GetAll");
                if (reloadCategoryResponse.IsSuccessStatusCode)
                {
                    var categoryContent = await reloadCategoryResponse.Content.ReadAsStringAsync();
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
                // Log the error
                Console.WriteLine($"Create error: {ex}");
                return Page();
            }
        }
    }
}
