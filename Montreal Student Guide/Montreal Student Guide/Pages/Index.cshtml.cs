using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Montreal_Student_Guide.Data;
using Montreal_Student_Guide.Models;
using Newtonsoft.Json.Linq;

namespace Montreal_Student_Guide.Pages
{
    public class IndexModel : PageModel
    {
      //  private readonly ApplicationDbContext _context;
       public List<Product> Products = new List<Product>();
       private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
          _httpClient = httpClient;
        }
        public async Task<IActionResult> OnGet()
        {
            var response = await _httpClient.GetAsync("https://localhost:7249/api/Products/Get");
            var values = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(values);
            foreach(var item in obj["products"])
            {
                Products.Add(new Product(item["studentId"].ToString(), item["opusCardFee"].ToString(),
                    Convert.ToInt32(item["avgRent"]), item["publicLibAdd"].ToString(), item["category"].ToString()));
            }
            return Page();
        }
    }
}