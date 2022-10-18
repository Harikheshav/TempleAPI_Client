using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class AdminController : Controller
    {
        Admin admin;
        string baseurl = "https://localhost:7264/api/Admin";
        public IActionResult login()
        {
            return View(admin);
        }
        [HttpPost]
        public async Task<IActionResult> login(Admin l)
        {
            Admin result;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/" + l.Uname + "?Pword=" + l.Pword))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Admin>(apiResponse);
                }
            }
            if (result != null)
            {
                HttpContext.Session.SetString("AdminName", result.Uname);
                HttpContext.Session.SetString("AdminEmail", result.Emailid);
                HttpContext.Session.SetString("AdminId", result.Uid.ToString());
                return RedirectToAction("Index", "AdminBook");
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
            return View(admin);
        }
        [HttpPost]
        public async Task<IActionResult> register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync(baseurl, content);
                    //response body makes no sense and is of no use...
                }
                return RedirectToAction("login");
            }
            else
                return View("register", admin);
        }
    }

}

