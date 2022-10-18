using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class FnHallBkngController : Controller
    {
        string baseurl = "https://localhost:7004/api/FnHallBkng";
        private string DateValidation(DateTime? sdt, DateTime? edt)
        {
            if (sdt <= DateTime.Now)
            {
                return "Cannot Assign Events on Past Date and Time";
            }
            if (edt <= sdt)
            {
                return "Event ends before it gets started";
            }
            else
                return null;

        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            FnHallBkng fbkng = new FnHallBkng();
            string valid = DateValidation(fbkng.Sdt, fbkng.Edt);
            if (valid != null)
            {
                ModelState.AddModelError("Error", valid);
                return View(fbkng);
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fbkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                }
            }
            return View(fbkng);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FnHallBkng fbkng)
        {
            FnHallBkng fnhallBkng = new FnHallBkng();

            using (var httpClient = new HttpClient())
            {
                int id = fbkng.Bkid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(fbkng), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(baseurl + "/" + fbkng.Bkid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    fbkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                }
            }
            return RedirectToAction("Index", "AdminBook");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            FnHallBkng fbkng = new FnHallBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fbkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                }
            }
            return View(fbkng);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(FnHallBkng fbkng)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseurl + "/" + fbkng.Bkid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "AdminBook");
        }
    }

}
