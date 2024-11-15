using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using Newtonsoft.Json;
using Test_2.DTO;

namespace Test_2.Pages.SilverJwery
{
    public class IndexModel : PageModel
    {

        public IList<SilverJewelry> SilverJewelry { get;set; } = new List<SilverJewelry>();

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (TempData["Message"] != null)
                {
                    Message = TempData["Message"].ToString();
                }

                var token = HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Index");
                }

                using(var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var query = new List<string>();
                    query.Add("$select=SilverJewelryId,SilverJewelryName,SilverJewelryDescription,MetalWeight,Price,ProductionYear,CreatedDate,CategoryId"); // Thêm CategoryId vào select
                    query.Add("$expand=Category");
                    query.Add("$count=true");
                    if (!string.IsNullOrEmpty(SearchName))
                    {
                        query.Add($"$filter=contains(SilverJewelryName, '{SearchName}')");
                    }

                    var queryString = string.Join("&", query);
                    var response = await httpClient.GetAsync($"http://localhost:5008/odata/SilverJewelrys?{queryString}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result  = JsonConvert.DeserializeObject<OdataDTO<SilverJewelry>>(content).Value;
                        SilverJewelry = result;
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
