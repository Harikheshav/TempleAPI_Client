using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class ConHallBkngController : Controller
    {
        string baseurl = "https://localhost:7282/api/ConHallBkng";
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ConHallBkng cbkng = new ConHallBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cbkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                }
            }
            return View(cbkng);
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
        public async Task<ActionResult> Edit(ConHallBkng cbkng)
        {
            ConHallBkng conhallBkng = new ConHallBkng();

            using (var httpClient = new HttpClient())
            {
                int id = cbkng.Bkid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(cbkng), Encoding.UTF8, "application/json");
                string valid = DateValidation(cbkng.Sdt, cbkng.Edt);
                if (valid!=null)
                {
                    ModelState.AddModelError("Error", valid);
                    return View(cbkng);
                }
                using (var response = await httpClient.PutAsync(baseurl + "/" + cbkng.Bkid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    cbkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                }
            }
            return RedirectToAction("Index", "AdminBook");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ConHallBkng cbkng = new ConHallBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cbkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                }
            }
            return View(cbkng);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(ConHallBkng cbkng)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseurl + "/" + cbkng.Bkid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "AdminBook");
        }
    }

}
