using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.DTO;

namespace PRN_ASS1_RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        [BindProperty]
        public LoginRequest Input { get; set; }
        public string ReturnURL { get; set; }


        public void OnGet(string returnURL = null)
        {
            ReturnURL = returnURL;
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync("http://localhost:5113/api/Auth/login", Input);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

                        HttpContext.Session.SetString("Token", result.TokenString);
                        HttpContext.Session.SetInt32("Role", (int)result.RoleId);
                        HttpContext.Session.SetInt32("AccountId", result.AccountId);

                        if (result.RoleId == 1)
                        {
                            return RedirectToPage("/Jewely/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "You do not have permission to do this function.");
                    }
                    return Page();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
