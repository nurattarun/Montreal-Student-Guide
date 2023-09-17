using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Montreal_Student_Guide.Models;
using Newtonsoft.Json.Linq;

namespace Montreal_Student_Guide.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        private readonly HttpClient _httpClient;
        public DeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7249/api/Products/Get/{id}");
            var values = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(values);
            Product = new Product(obj["studentId"].ToString(), obj["opusCardFee"].ToString(), Convert.ToInt32(obj["avgRent"]),  obj["publicLibAdd"].ToString(), obj["category"].ToString());
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7249/api/Products/Delete/{Product.StudentId}");
            var values = await response.Content.ReadAsStringAsync();

            return RedirectToPage("Index");
        }
    }
}