using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Montreal_Student_Guide.Models;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace Montreal_Student_Guide.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]

        public Product Product { get; set;}
        private readonly HttpClient _httpClient;
        public string Error { get; set; }
       
        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

                var response = await _httpClient.PostAsync("https://localhost:7249/api/Products/Post", jsonContent);
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
}
