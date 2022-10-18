using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class AnDhanBkngController : Controller
    {
        string baseurl = "https://localhost:7201/api/AnDhanBkng";
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            AnDhanBkng abkng = new AnDhanBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                }
            }
            return View(abkng);
        }
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
        [HttpPost]
        public async Task<ActionResult> Edit(AnDhanBkng abkng)
        {
            AnDhanBkng andhanBkng = new AnDhanBkng();
            string valid = DateValidation(abkng.Sdt, abkng.Edt);
            if (valid != null)
            {
                ModelState.AddModelError("Error", valid);
                return View(abkng);
            }
            using (var httpClient = new HttpClient())
            {
                int id = abkng.Bkid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(abkng), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(baseurl + "/" + abkng.Bkid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    andhanBkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                }
            }
            return RedirectToAction("Index", "AdminBook");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            AnDhanBkng abkng = new AnDhanBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                }
            }
            return View(abkng);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(AnDhanBkng abkng)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(baseurl + "/" + abkng.Bkid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction("Index", "AdminBook");
            }
            return View(abkng);
            
        }
    }

}
