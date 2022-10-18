using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class UserController : Controller
    {
        User user;
        string baseurl = "https://localhost:7285/api/User";
        public IActionResult login()
        {
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> login(User l)
        {
            User result;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/"+l.Uname+"?Pword=" + l.Pword))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            if (result != null)
            {
                HttpContext.Session.SetString("UserName", result.Uname);
                HttpContext.Session.SetString("EmailId", result.Emailid);
                HttpContext.Session.SetString("UserId", result.Uid.ToString());
                return RedirectToAction("Index", "Userbook");
            }
            else
                return View();
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        public IActionResult register()
        {
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> register(User user)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync(baseurl, content);
                    //response body makes no sense and is of no use...
                }
                return RedirectToAction("login");
            }
            else
                return View("register",user);
        }
    }
}

