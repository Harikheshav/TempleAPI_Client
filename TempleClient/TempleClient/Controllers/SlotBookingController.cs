using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;
namespace TempleClient.Controllers
{
    public class SlotBookingController : Controller
    {
        SlotBkng slot = new SlotBkng();
        string baseurl = "https://localhost:7060/api/SlotBooking";
        public IActionResult Index()
        {
            return View(slot);
        }
        [HttpPost]
        public async Task<IActionResult> Index(SlotBkng slot)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(slot), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync(baseurl, content);
                    //response body makes no sense and is of no use...
                    TempData["Slot_Status"] = "Slot Added Succesfully!!!";
                }
                return RedirectToAction("Index");
                
            }
            else
                return View("Index", slot);
        }
    }
}
