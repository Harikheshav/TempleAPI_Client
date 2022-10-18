using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}