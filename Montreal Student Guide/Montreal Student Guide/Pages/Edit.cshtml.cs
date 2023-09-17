using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Montreal_Student_Guide.Models;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace Montreal_Student_Guide.Pages;
 public class EditModel : PageModel
{
    [BindProperty]
    public Product Product { get; set; }

    private readonly HttpClient _httpClient;

    public string Error { get; set; }
    public EditModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IActionResult> OnGet(string id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7249/api/Products/Get/{id}");
        var values = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(values);
        Product = new Product(obj["studentId"].ToString(), obj["opusCardFee"].ToString(), Convert.ToInt32(obj["avgRent"]), obj["publicLibAdd"].ToString(), obj["category"].ToString());
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            StringContent jsonContent = new StringContent(
            JsonSerializer.Serialize(Product),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _httpClient.PutAsync($"https://localhost:7249/api/Products/Put/{Product.StudentId}", jsonContent);
            var values = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(values);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                Error = obj["error_Message"].ToString();
            }
        }


        return Page();
    }
}
