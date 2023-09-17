using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Montreal_Student_Guide.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Montreal_Student_Guide.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]


        public Product Product { get; set; }
        private readonly HttpClient _httpClient;

        public string Error { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                return Page();
            };

            var jsonContent = new StringContent(
        JsonSerializer.Serialize(User),
        Encoding.UTF8,
        "application/json");


            var response = await _httpClient.PostAsync(
           "https://localhost:7249/api/Login",
           jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }

        public class LoginInputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}

//    var response = await _httpClient.PutAsync($"https://localhost:7249/api/Login/Login", jsonContent);
//                var values = await response.Content.ReadAsStringAsync();
//                var obj = JObject.Parse(values);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToPage("Login");
//                }
//                else
//                {
//                    Error = obj["error_Message"].ToString();
//                }
//            }


//            return Page();
//        }
//    }
//}

//        public void OnGet()
//        {
//        }
//    }
//}
